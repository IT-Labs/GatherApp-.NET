# GatherApp – API

## Table of Contents

1.  [Introduction](#introduction)

2.  [Features](#features)

3.  [Technologies](#technologies)

4.  [Architecture and Pattern](#architecture-and-pattern)

5.  [Docker Setup](#docker-setup)

6.  [Setup Locally](#setup-locally)

7.  [How to Contribute to the Project](#how-to-contribute-to-the-project)

---

**IMPORTANT: Credit where credit is due. This app is the end product of the amazing work put in by the participants in our latest Internship program – showing off their amazing technical skills, but also ability to truly cooperate and collaborate!**

## Introduction

This application is the back-end part of GatherApp project that enables employees to create a variety of events, differing in theme, location, type, and guest list both on behalf of the company and the employee as an individual. The front-end part of this project can be found on this link **[here](https://github.com/IT-Labs/GatherApp-UI)**.

## Features

User and Admin:

- **Authentication Options**: Support for custom login or Microsoft Single Sign-On (SSO).
- **Attend Public or Invited Events**: Users can attend public events or those they've been personally invited to.
- **Integrated Calendar**: Users have access to a built-in calendar within the application for managing their events.
- **Outlook Calendar Integration**: Events can seamlessly sync with Outlook calendars. If not logged in, users are prompted to authenticate before adding events to Outlook.

User Role:

- **Create Events**: Users can create individual events which require approval, unless scheduled on weekends.

Admin Role:

- **Create Events**: Admins can create individual or company events which are automatically approved.
- **Event Approval**: Admins have the authority to approve or decline event requests from users.

For more detailed informations, check our user guide [here](https://github.com/IT-Labs/GatherApp-.NET/blob/main/GatherApp-Userguide.pdf).

## Technologies

This application uses .NET 8 Web API project that uses Entity Framework 8.0.2 to communicate with PostgreSQL DB. The database will be automatically created by setting the **ConnectionStrings** in **appsettings.Development.json** file. Then, execute the EF migrations with **update-database** in Package Manager Console, or use [docker setup](#docker-setup). The repository already contains Azure Pipeline script that can be used for setting up CI/CD on Azure DevOps. The application uses custom JWT tokens for authentication/authorization. The SQL Server RDBMS should be used, and the application will use self-issued JWT tokens for authentication/authorization. Swagger is used for application documentation.

## Architecture and Pattern

### N-Layered Architecture

N-Layered architecture is a software design pattern that organizes code into multiple layers, each responsible for a specific aspect of the application's functionality. The layers that we use are API, Contracts, DataContext, Repositories, and Services. This architecture promotes modularity, maintainability, and separation of concerns by enforcing strict boundaries between different layers. Each layer communicates with adjacent layers through well-defined interfaces, allowing for easier testing, scalability, and code reuse.

### Unit of Work Pattern

The Unit of Work pattern is a design pattern used to manage transactions and database operations in a modular and consistent manner. It encapsulates multiple database operations into a single unit of work, ensuring that all operations either succeed or fail together. This pattern is commonly used in conjunction with the Repository pattern, where the Unit of Work coordinates transactions across multiple repositories. By abstracting away the details of transaction management, the Unit of Work pattern simplifies code, enhances maintainability, and improves the overall integrity of database operations.

## Docker Setup

Prerequesite: Docker installed on your system. You can download and install Docker Desktop from [here](https://www.docker.com/products/docker-desktop/).

```bash
git clone https://github.com/IT-Labs/GatherApp-.NET.git
```

Setup your [appsettings.Development.json](#setup-locally)

```bash
cd GatherApp-.NET
```

Make changes to docker-compose.yml file if needed

```bash
docker-compose build
```

To run the application

```bash
docker-compose up
```

and navigate to **localhost:8080/swagger** or to stop the application

```bash
docker-compose down
```

## Setup Locally

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
