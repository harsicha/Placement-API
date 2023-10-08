using CapitalPlacement.DatabaseModels;
using CapitalPlacement.DataTransferModels;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using File = CapitalPlacement.DatabaseModels.File;

namespace CapitalPlacement.DataLayer
{
    public class DBContext
    {
        private readonly Container _applications;
        private readonly Container _addtionalQuestions;
        private readonly Container _experiences;
        private readonly Container _files;
        private readonly Container _stages;

        public DBContext(IOptions<DBSettings> settings)
        {
            CosmosClientOptions options = new()
            {
                HttpClientFactory = () => new HttpClient(new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }),
                ConnectionMode = ConnectionMode.Gateway,
                LimitToEndpoint = true
            };
            CosmosClient client = new(
                accountEndpoint: settings.Value.Endpoint,
                authKeyOrResourceToken: settings.Value.Key,
                options
            );
            var dbTask = client.CreateDatabaseIfNotExistsAsync(id: "capital");
            dbTask.Wait();
            Database database = dbTask.Result;

            var contTask = database.CreateContainerIfNotExistsAsync(id: "applications", partitionKeyPath: "/id");
            contTask.Wait();
            _applications = contTask.Result;

            contTask = database.CreateContainerIfNotExistsAsync(id: "addtionalQuestions", partitionKeyPath: "/id");
            contTask.Wait();
            _addtionalQuestions = contTask.Result;

            contTask = database.CreateContainerIfNotExistsAsync(id: "experiences", partitionKeyPath: "/id");
            contTask.Wait();
            _experiences = contTask.Result;

            contTask = database.CreateContainerIfNotExistsAsync(id: "files", partitionKeyPath: "/id");
            contTask.Wait();
            _files = contTask.Result;

            contTask = database.CreateContainerIfNotExistsAsync(id: "stages", partitionKeyPath: "/id");
            contTask.Wait();
            _stages = contTask.Result;
        }

        /// <summary>
        /// Create a new applicationId and store the Application in the database.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public async Task<string?> CreateApplication(Application application)
        {
            try
            {
                application.Id = Guid.NewGuid().ToString();
                await _applications.CreateItemAsync(application, new PartitionKey(application.Id));
                return application.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in CreateApplication method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get the application stored in DB by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Application?> GetApplication(string id)
        {
            try
            {
                var application = await _applications.ReadItemAsync<Application>(id, partitionKey: new PartitionKey(id));
                return application;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetApplication method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Update the existing application in DB. Valid applicationId must be passed.
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateApplication(Application application)
        {
            try
            {
                var isSuccess = Guid.TryParse(application.Id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", application.Id);
                }
                application.ModifiedOn = DateTime.Now;
                await _applications.UpsertItemAsync(application, partitionKey: new PartitionKey(application.Id));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateApplication method of DBContext: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Save an additional question in DB.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<string?> CreateAdditionalQuestion(AdditionalQuestion question)
        {
            try
            {
                question.Id = Guid.NewGuid().ToString();
                foreach (var choice in question.Choices)
                {
                    choice.Id = Guid.NewGuid().ToString();
                }
                await _addtionalQuestions.CreateItemAsync(question, new PartitionKey(question.Id));
                return question.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in CreateAdditionalQuestion method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get the additional questions based on the applicationId
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<AdditionalQuestion>> GetAdditionalQuestionsByAppId(string appId)
        {
            List<AdditionalQuestion> questions = new();
            try
            {
                var isSuccess = Guid.TryParse(appId, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", appId);
                }
                var query = new QueryDefinition(
                    query: "SELECT * FROM c WHERE c.applicationId = @appId"
                )
                .WithParameter("@appId", appId);
                using FeedIterator<AdditionalQuestion> feed = _addtionalQuestions.GetItemQueryIterator<AdditionalQuestion>(queryDefinition: query);
                while (feed.HasMoreResults)
                {
                    FeedResponse<AdditionalQuestion> response = await feed.ReadNextAsync();
                    foreach (var question in response)
                    {
                        questions.Add(question);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetAdditionalQuestionsByAppId method of DBContext: " + ex.ToString());
            }
            return questions;
        }

        /// <summary>
        /// Update the existing question in DB by Id.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateAdditionalQuestion(AdditionalQuestion question)
        {
            try
            {
                var isSuccess = Guid.TryParse(question.Id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", question.Id);
                }
                await _addtionalQuestions.UpsertItemAsync(question, partitionKey: new PartitionKey(question.Id));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateAdditionalQuestion method of DBContext: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Save a stage in DB.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        public async Task<string?> CreateStage(Stage stage)
        {
            try
            {
                stage.Id = Guid.NewGuid().ToString();
                await _stages.CreateItemAsync(stage, new PartitionKey(stage.Id));
                return stage.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in CreateStage method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all stages related to the passed applicationId.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Stage>> GetStagesByAppId(string appId)
        {
            List<Stage> stages = new();
            try
            {
                var isSuccess = Guid.TryParse(appId, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", appId);
                }
                var query = new QueryDefinition(
                    query: "SELECT * FROM c WHERE c.applicationId = @appId"
                )
                .WithParameter("@appId", appId);
                using FeedIterator<Stage> feed = _stages.GetItemQueryIterator<Stage>(queryDefinition: query);
                while (feed.HasMoreResults)
                {
                    FeedResponse<Stage> response = await feed.ReadNextAsync();
                    foreach (var stage in response)
                    {
                        stages.Add(stage);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetStagesByAppId method of DBContext: " + ex.ToString());
            }
            return stages;
        }

        /// <summary>
        /// Update an existing stage in DB by Id.
        /// </summary>
        /// <param name="stage"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateStage(Stage stage)
        {
            try
            {
                var isSuccess = Guid.TryParse(stage.Id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", stage.Id);
                }
                await _stages.UpsertItemAsync(stage, partitionKey: new PartitionKey(stage.Id));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateStage method of DBContext: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Save the Experience object in DB.
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        public async Task<string?> CreateExperience(Experience experience)
        {
            try
            {
                experience.Id = Guid.NewGuid().ToString();
                await _experiences.CreateItemAsync(experience, new PartitionKey(experience.Id));
                return experience.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in CreateExperience method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all the experience objects related to the passed applicationId.
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Experience>> GetExperiencesByAppId(string appId)
        {
            List<Experience> experiences = new();
            try
            {
                var isSuccess = Guid.TryParse(appId, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", appId);
                }
                var query = new QueryDefinition(
                    query: "SELECT * FROM c WHERE c.applicationId = @appId"
                )
                .WithParameter("@appId", appId);
                using FeedIterator<Experience> feed = _experiences.GetItemQueryIterator<Experience>(queryDefinition: query);
                while (feed.HasMoreResults)
                {
                    FeedResponse<Experience> response = await feed.ReadNextAsync();
                    foreach (var stage in response)
                    {
                        experiences.Add(stage);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetExperiencesByAppId method of DBContext: " + ex.ToString());
            }
            return experiences;
        }

        /// <summary>
        /// Update an experience record in DB.
        /// </summary>
        /// <param name="experience"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateExperience(Experience experience)
        {
            try
            {
                var isSuccess = Guid.TryParse(experience.Id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", experience.Id);
                }
                await _experiences.UpsertItemAsync(experience, partitionKey: new PartitionKey(experience.Id));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateExperience method of DBContext: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Save a file in base64 format in DB.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string?> CreateFile(File file)
        {
            try
            {
                file.Id = Guid.NewGuid().ToString();
                await _files.CreateItemAsync(file, new PartitionKey(file.Id));
                return file.Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in CreateFile method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get an existing file from DB by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<File?> GetFile(string id)
        {
            try
            {
                var isSuccess = Guid.TryParse(id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", id);
                }
                File file = await _files.ReadItemAsync<File>(id, new PartitionKey(id));
                return file;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in GetFile method of DBContext: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Update an existing file record.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateFile(File file)
        {
            try
            {
                var isSuccess = Guid.TryParse(file.Id, out Guid guid);
                if (!isSuccess)
                {
                    throw new ArgumentException("Id must be a valid Guid", file.Id);
                }
                await _files.UpsertItemAsync(file, partitionKey: new PartitionKey(file.Id));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in UpdateFile method of DBContext: " + ex.ToString());
                return false;
            }
        }
    }
}
