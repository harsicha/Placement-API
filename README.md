# CapitalPlacement  

## Description  

+ The application has been built by keeping in mind the four main tabs in the requirement
+ Below are the 5 Cosmos DB containers used:
  1. applications - contains all the information from the Program Details tab and also some information from the Application Form tab such as the coverImageId(File is stored in files container), Personal Information(except additional questions) and Profile(except additional questions)
  2. addtionalQuestions - contains all the additional questions. Data is segregated based on IsPersonal, IsProfile and IsAdditional boolean fields  
  3. experiences - contains the education and work experience information. Data is segregated based on IsEducation boolean field  
  4. files - contains all the files including the cover image and the file in the File Upload option of the addition questions. Files are stored in base64 format and the maximum file size is 1 MB  
  5. stages - contains all the stage information in the workflow tab  

## Endpoints  

+ [POST] https://localhost:44307/api/program - Post and save the Program related data. This endpoint returns the applicationId which must be used across all the tabs to further process information.  
+ [GET] https://localhost:44307/api/program?appId=55f7c12d-b356-410f-8391-918cc0530c2c - Get all data related to the Program Tab.  
+ [PUT] https://localhost:44307/api/program - Update the program details. ApplicationId is required.  
+ [PUT] https://localhost:44307/api/application - Post and save all the details related to the Application tab. ApplicationId is required.  
  + id is required for the updating. For updating the experience and additional questions pass the relevant Ids like personalInformation.additionalQuestions[0].id, profile.experiences[0].id, profile.additionalQuestions[0].id, additionalQuestions[0].id, etc. and the relevant fileIds too if no file refresh is required. Please refer the postman collection for all the id fields.
+ [GET] https://localhost:44307/api/application - Get all details related to the Application Tab. ApplicationId is required.  
+ [PUT] https://localhost:44307/api/workflow - Post and save the stages. ApplicationId is required.  
+ [GET] https://localhost:44307/api/workflow - Get a list of all stages related to the applicationId.  

Please find the postman collection [here](https://drive.google.com/file/d/1ckIs7mmzQ7HOLoJXrJv3xzbqRqmWLPW8/view?usp=sharing)  

### Postman Application testing guide  

1. Use the POST Program endpoint to save the information and note the applicationId in response.  
2. Use the GET Program endpoint to get the stored information using the applicationId.  
3. Use the PUT Program endpoint to update the existing information. Make sure that the applicationId is passed in the id field.  
4. Use the PUT Application endpoint to save the information found on Application Form tab like personal information, profile, additional questions and cover image. Make sure that the applicationId is passed in the id field.  
5. [Optional] Use the PUT Application endpoint again to update the stored information. This time pass all the relevant id fields in the payload to make sure that the information is updated. Relevant ids can be found in the response of step 4.  
6. Use the PUT Workflow endpoint to store the stages.  
7. Use the GET Workflow endpoint to get the stored stages. ApplicationId is required.  
8. [Optional] Use the PUT Workflow endpoint again by supplying the id field in each stage object in the payload to update the existing record/s. Ids can be found in the response of step 6.  

### Limitations  

+ Blob storage on cloud could've been leveraged instead of directly storing the files in base64 in DB. Reason for not implementing in this project is limited time and resources  
+ Due to limited resources Azure Cosmos DB Emulator has been used for development

## Technologies used  

+ ASP.Net Core 7  
+ Azure Cosmos DB for NoSQL  

## Tools used  

+ Visual Studio 2022 Community Edition  
+ Docker for Cosmos DB  
+ GitHub
+ Postman  
