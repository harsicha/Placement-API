using AutoMapper;
using CapitalPlacement.Abstracts;
using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataLayer;
using CapitalPlacement.DataTransferModels;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Constant = CapitalPlacement.Constants.Constants;
using File = CapitalPlacement.DatabaseModels.File;

namespace CapitalPlacement.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public ApplicationService(DBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the existing application record from DB. ApplicationId is required.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<dynamic> GetAsync(string appId)
        {
            try
            {
                if (string.IsNullOrEmpty(appId))
                {
                    return "Please provide an applicationId";
                }
                var isSuccess = Guid.TryParse(appId, out Guid res);
                if (!isSuccess)
                {
                    return "Please provide a valid applicationId";
                }
                
                // Get the existing application record from DB
                var application = await _dbContext.GetApplication(appId);
                // Get all the experience records from DB
                var experiences = await _dbContext.GetExperiencesByAppId(appId);
                // Get all the additional questions from DB
                var additionalQuestions = await _dbContext.GetAdditionalQuestionsByAppId(appId);

                if (application == null)
                {
                    return "Please provide a valid applicationId";
                }

                List<AdditionalQuestionDTO> profileAddQuestions = new();
                List<AdditionalQuestionDTO> personalInfoAddQuestions = new();
                List<AdditionalQuestionDTO> addQuestions = new();
                
                // Segregate the additional questions according to the IsProfile, Ispersonal and IsAdditional boolean fields
                foreach (var addQuestion in additionalQuestions)
                {
                    var mapped = _mapper.Map<AdditionalQuestionDTO>(addQuestion);
                    if (!string.IsNullOrEmpty(mapped.FileId))
                    {
                        mapped.FileResponse = await _dbContext.GetFile(mapped.FileId);
                    }
                    if (addQuestion.IsProfile)
                    {
                        profileAddQuestions.Add(mapped);
                    }
                    else if (addQuestion.IsPersonal)
                    {
                        personalInfoAddQuestions.Add(mapped);
                    }
                    else if (addQuestion.IsAdditional)
                    {
                        addQuestions.Add(mapped);
                    }
                }

                // Create a data transfer object for returning the relevant data to the client
                ApplicationDTO dto = new()
                {
                    Id = appId,
                    CoverImageId = application.CoverImageId,
                    PersonalInformation = _mapper.Map<PersonalInformationDTO>(application.PersonalInformation),
                    Profile = _mapper.Map<ProfileDTO>(application.Profile)
                };
                dto.PersonalInformation.AdditionalQuestions = personalInfoAddQuestions;
                dto.Profile.Experiences = experiences;
                dto.Profile.AdditionalQuestions = profileAddQuestions;
                dto.AdditionalQuestions = addQuestions;

                if (!string.IsNullOrEmpty(application.CoverImageId))
                {
                    dto.FileResponse = await _dbContext.GetFile(application.CoverImageId);
                }

                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetAsync method of ApplicationService: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Update the existing application record in DB. Valid applicationId must be supplied.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateAsync(ApplicationDTO application)
        {
            try
            {
                // Get the existing application record from DB
                var existingDoc = await _dbContext.GetApplication(application.Id);
                if (existingDoc == null)
                {
                    return "Invalid applicationId";
                }
                string? fileId = null;
                // Check if the file size exceeds the maximum limit
                if (application.File != null && application.File.Length > Constant.FILE_SIZE_MAX_BYTES)
                {
                    return "File size cannot exceed 1 MB";
                }
                // Upload new cover image file to DB
                if (application.File != null && string.IsNullOrEmpty(application.CoverImageId))
                {
                    fileId = await UploadFile(application.File);
                }

                // Updating the existing document in DB to accomodate for the Application Tab data
                existingDoc.CoverImageId = string.IsNullOrEmpty(fileId) ? existingDoc.CoverImageId : fileId;
                application.CoverImageId = existingDoc.CoverImageId;
                existingDoc.PersonalInformation = _mapper.Map<PersonalInformation>(application.PersonalInformation);
                existingDoc.Profile = _mapper.Map<DatabaseModels.Profile>(application.Profile);
                await _dbContext.UpdateApplication(existingDoc);

                // Adding Education/Experience related data to DB
                foreach (var experience in application.Profile.Experiences)
                {
                    if (string.IsNullOrEmpty(experience.ApplicationId))
                    {
                        experience.ApplicationId = application.Id;
                    }
                    if (string.IsNullOrEmpty(experience.Id))
                    {
                        string? id = await _dbContext.CreateExperience(experience);
                        if (string.IsNullOrEmpty(id))
                        {
                            return "Something went wrong";
                        }
                        experience.Id = id;
                    }
                    else
                    {
                        await _dbContext.UpdateExperience(experience);
                    }
                }

                // Adding additional questions to DB
                await UploadAdditionalQuestions(application.AdditionalQuestions, application.Id);
                await UploadAdditionalQuestions(application.PersonalInformation.AdditionalQuestions, application.Id);
                await UploadAdditionalQuestions(application.Profile.AdditionalQuestions, application.Id);

                return application;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateAsync method of ApplicationService: " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Uitility method used to upload data to DB
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<string?> UploadFile(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            File fileObj = new()
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                IsActive = true,
                Value = Convert.ToBase64String(bytes)
            };
            var id = await _dbContext.CreateFile(fileObj);
            return id;
        }

        /// <summary>
        /// Uitility method used to upload additional questions to DB
        /// </summary>
        /// <param name="additionalQuestions"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        private async Task<string> UploadAdditionalQuestions(List<AdditionalQuestionDTO> additionalQuestions, string appId)
        {
            foreach (var question in additionalQuestions)
            {
                if (string.IsNullOrEmpty(question.ApplicationId))
                {
                    question.ApplicationId = appId;
                }
                foreach (var item in additionalQuestions)
                {
                    if (item.File != null && string.IsNullOrEmpty(item.FileId))
                    {
                        var id = await UploadFile(item.File);
                        if (string.IsNullOrEmpty(id))
                        {
                            return "Something went wrong";
                        }
                        item.FileId = id;
                    }
                }
                AdditionalQuestion additionalQuestion = _mapper.Map<AdditionalQuestion>(question);
                if (string.IsNullOrEmpty(question.Id))
                {
                    string? id = await _dbContext.CreateAdditionalQuestion(additionalQuestion);
                    if (string.IsNullOrEmpty(id))
                    {
                        return "Something went wrong";
                    }
                    question.Id = id;
                }
                else
                {
                    await _dbContext.UpdateAdditionalQuestion(additionalQuestion);
                }
            }
            
            return string.Empty;
        }
    }
}
