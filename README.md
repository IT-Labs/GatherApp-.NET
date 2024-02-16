# GatherApp – API

## Table of Contents

1.  [Introduction](#introduction)

2.  [Technologies](#technologies)

3.  [Setup](#setup)

4.  [How to Contribute to the Project](#how-to-contribute-to-the-project)

---

## Introduction

This application is the back-end part of GatherApp project that enables employees to create a variety of events, differing in theme, location, type, and guest list both on behalf of the company and the employee as an individual. The front-end part of this project can be found on this link **[here](https://github.com/IT-Labs/GatherApp-UI)**.

## Technologies

This application uses .NET 8 Web API project that uses Entity Framework 8.0.1 to communicate with PostgreSQL DB. The database will be automatically created by setting the **ConnectionStrings** in **appsettings.Development.json** file. Then, execute the EF migrations with **update-database** in Package Manager Console. The repository already contains Azure Pipeline script that can be used for setting up CI/CD on Azure DevOps. The application uses custom JWT tokens for authentication/authorization. The SQL Server RDBMS should be used, and the application will use self-issued JWT tokens for authentication/authorization. Swagger is used for application documentation.

## Setup

- Install PostgreSQL server locally
- Clone Repository from Git
- Open the solution in Visual Studio 2023
- In appsettings.Development.json file add the following properties:
  - ConnectionStrings:
    - WebApiDatabase: Connection string to the local database.
  - EmailSettings:
    - Host: SMTP host for sending emails.
    - Username: Username for email authentication.
    - Password: Password for email authentication.
    - EnableSsl: Indicates whether SSL should be enabled for email communication.
  - AzureKeyVaultSettings:
    - KeyVaultName: Name of the Azure Key Vault.
    - AzureTentantId: Tenant ID for accessing Azure resources.
    - AzureClientId: Client ID for accessing Azure resources.
    - AzureClientSecretId: Client secret for accessing Azure resources.
    - UseAzureKeyVault: Boolean indicating whether to use Azure Key Vault for storing secrets.
  - Urls:
    - UiUrl: URL of the user interface.
    - ResetPasswordUrl: URL for resetting passwords.
    - ServerUrl: URL of the API server.
  - BlobStorageSettings:
    - AzureStorage: Azure Blob Storage connection string.
    - AzurePath: Path for storing blobs in Azure Blob Storage.
  - JWT:
    - Issuer: Issuer of the JWT token.
    - Audience: Audience of the JWT token.
    - AccessExpiresInMinutes: Duration in minutes for which the access token remains valid.
    - RefreshExpiresInMinutes: Duration in minutes for which the refresh token remains valid.
  - Serilog:
    - Using: Indicates the Serilog sinks being used.
    - MinimumLevel: Minimum log level to be recorded.
    - WriteTo: Specifies where the logs should be written, e.g., ApplicationInsights, Console, File.
- Run the application

**NOTE**:
In appsettings.json file, these values are stored in Azure Key Vault, and AzureKeyVaultSettings are stored in Library variables on Azure DevOps and are initialized when the backend pipeline is triggered.

## How to Contribute to the Project

To contribute to the project:

1. Fork this repository.

2. Clone your forked repository.

3. Make your changes.

4. Commit and push your changes.

5. Create a pull request.

6. Star this repository.

7. Wait for the pull request to be merged.
