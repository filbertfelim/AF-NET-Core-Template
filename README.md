# Azure Function ASP.NET Core Integration Template


This repository is used as project templating for Azure Function ASP.NET Core Integration project, using shell script to create the multiple .NET project and project structuring, as well as T4 template files for file templating. 

Project Structure
---
This template provides project structure with 3 layers, API, Service, and Data Access Layer (DAL)  before fetching data from data store. 
```mermaid
flowchart TD 
	func[API / Functions] --> service[Service] --> repository[DAL] --> db[(Database)]
   ```
    

How to Create the Project Template
---
1. Run the shell script with this command below :
	```console
	./setup.sh
	```
2. This shell script will automatically execute and manage the project structuring and also create necessary project files such as API, Service, Repository, and test files.
3. The project by default will create the project files related to the **Product** model.

The default value for the T4 template is :
- **entityName** ( the name of the entity to create the template for ) : **Product**
- **rootFolder** ( the name of the root folder directory ) : **TestProject**
- **ApiProjectName** ( the name of the API / Functions project ) : **Api**
- **ServiceProjectName** ( the name of the Service project ) : **Service**
- **DALProjectName** ( the name of the DAL project ) : **DAL**

In the shell script, there are also multiple modifiable variables with the default value : 
- **SOLUTION_NAME** ( the name of the solution for the projects ) : **MySolution**
- **API_PROJECT_NAME** ( the name of the API / Functions project ) : **Api**
- **SERVICE_PROJECT_NAME** ( the name of the Service project ) : **Service**
- **DAL_PROJECT_NAME** ( the name of the DAL project ) : **DAL**
---
- **ApiProjectName** should be the same as **API_PROJECT_NAME**
- **ServiceProjectName** should be the same as **SERVICE_PROJECT_NAME**
- **DalProjectName** should be the same as **DAL_PROJECT_NAME**
