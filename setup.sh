#!/bin/bash

# Set the solution and project names
SOLUTION_NAME="MySolution"
API_PROJECT_NAME="Api"
SERVICE_PROJECT_NAME="Service"
DAL_PROJECT_NAME="DAL"
API_TEST_PROJECT_NAME="${API_PROJECT_NAME}.Tests"
SERVICE_TEST_PROJECT_NAME="${SERVICE_PROJECT_NAME}.Tests"
DAL_TEST_PROJECT_NAME="${DAL_PROJECT_NAME}.Tests"

# Create a new solution
dotnet new sln -n $SOLUTION_NAME

# Create the API project (Azure Functions)
func init $API_PROJECT_NAME --worker-runtime dotnet-isolated --target-framework net8.0

# Create the Service project (Class Library)
dotnet new classlib -n $SERVICE_PROJECT_NAME -f net8.0

# Create the DAL project (Class Library)
dotnet new classlib -n $DAL_PROJECT_NAME -f net8.0

# Add the projects to the solution
dotnet sln $SOLUTION_NAME.sln add $API_PROJECT_NAME/$API_PROJECT_NAME.csproj
dotnet sln $SOLUTION_NAME.sln add $SERVICE_PROJECT_NAME/$SERVICE_PROJECT_NAME.csproj
dotnet sln $SOLUTION_NAME.sln add $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj

# Add project references
dotnet add $API_PROJECT_NAME/$API_PROJECT_NAME.csproj reference $SERVICE_PROJECT_NAME/$SERVICE_PROJECT_NAME.csproj
dotnet add $API_PROJECT_NAME/$API_PROJECT_NAME.csproj reference $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj
dotnet add $SERVICE_PROJECT_NAME/$SERVICE_PROJECT_NAME.csproj reference $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj

# Add functions directory and class
cd $API_PROJECT_NAME
dotnet add package Microsoft.AspNetCore.Http.Abstractions
dotnet add package Newtonsoft.Json
mkdir Functions
cd ..

# add service and interface folder
cd $SERVICE_PROJECT_NAME
rm Class1.cs
mkdir Services
mkdir Interfaces
cd ..

# add repository, DTO and interface folder
cd $DAL_PROJECT_NAME
rm Class1.cs
mkdir DTO
mkdir Models
mkdir Repositories
mkdir Interfaces
cd ..

# Create the API test project
dotnet new xunit -n $API_TEST_PROJECT_NAME -f net8.0
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj reference $API_PROJECT_NAME/$API_PROJECT_NAME.csproj
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj reference $SERVICE_PROJECT_NAME/$SERVICE_PROJECT_NAME.csproj
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj reference $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj

# Add package to API test project
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj package Moq
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj package Microsoft.AspNetCore.Http.Abstractions
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj package Microsoft.AspNetCore.Mvc
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj package Newtonsoft.Json

# Create the Service test project
dotnet new xunit -n $SERVICE_TEST_PROJECT_NAME -f net8.0
dotnet add $SERVICE_TEST_PROJECT_NAME/$SERVICE_TEST_PROJECT_NAME.csproj reference $SERVICE_PROJECT_NAME/$SERVICE_PROJECT_NAME.csproj
dotnet add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj reference $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj

# Add Moq package to Service test project
dotnet add $SERVICE_TEST_PROJECT_NAME/$SERVICE_TEST_PROJECT_NAME.csproj package Moq

# Create the Repository test project
dotnet new xunit -n $DAL_TEST_PROJECT_NAME -f net8.0
dotnet add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj reference $DAL_PROJECT_NAME/$DAL_PROJECT_NAME.csproj

# Add package to Repository test project
dotnet add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj package Moq
dotnet add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj package Microsoft.AspNetCore.Http.Abstractions
dotnet add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj package Microsoft.AspNetCore.Mvc
dotnet add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj package Newtonsoft.Json

# Add test projects to the solution
dotnet sln $SOLUTION_NAME.sln add $API_TEST_PROJECT_NAME/$API_TEST_PROJECT_NAME.csproj
dotnet sln $SOLUTION_NAME.sln add $SERVICE_TEST_PROJECT_NAME/$SERVICE_TEST_PROJECT_NAME.csproj
dotnet sln $SOLUTION_NAME.sln add $DAL_TEST_PROJECT_NAME/$DAL_TEST_PROJECT_NAME.csproj

cd $API_TEST_PROJECT_NAME
rm UnitTest1.cs
mkdir Tests
cd ..

cd $SERVICE_TEST_PROJECT_NAME
rm UnitTest1.cs
mkdir Tests
cd ..

cd $DAL_TEST_PROJECT_NAME
rm UnitTest1.cs
mkdir Tests
cd ..

cd t4template
t4 GenerateCrudFunction.tt
t4 GeneratecrudServiceInterface.tt
t4 GenerateCrudService.tt
t4 GenerateCrudRepositoryInterface.tt
t4 GenerateEntityModel.tt
t4 GenerateCrudRepository.tt
t4 GenerateCrudDto.tt
t4 AppendDependencyInjection.tt
t4 GenerateApiUnitTest.tt
t4 GenerateServiceUnitTest.tt
t4 GenerateRepositoryUnitTest.tt

echo "Solution and projects created successfully."