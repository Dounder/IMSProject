# Integrated Management System (IMS) API - Clean Architecture

This is a sample project for demonstrating the Clean Architecture approach to building web APIs with .NET Core.

## Layers explained
* **Application Layer**: Contains application business logic and use cases.
* **Domain Layer**: Includes domain entities, enums, exceptions, and interfaces.
* **Infrastructure Layer**: Implements persistence and infrastructure concerns.
* **WebApi Layer**: The entry point for the web API, containing controllers and middleware.

### Getting Started
To get a local copy up and running follow these simple steps.

Note: This project is configured to run using Docker Compose. If you don't have Docker installed, you can download it [here](https://www.docker.com/products/docker-desktop).

### Prerequisites

- .NET 8.0 SDK
- Docker

## Docker Compose Setup

This project is configured to run using Docker Compose, which simplifies the setup process and ensures that the application runs in a consistent environment. There are separate Docker Compose files for different environments:

- `compose.yml`: This is the base Docker Compose file that defines the services required to run the application. Currently, it specifies the database (`db`) service.

- `compose.local.yml`: This file extends `compose.yml` for local development purposes. It adds the API service with hot reloading enabled, making it ideal for developers who need to see changes in real-time without rebuilding the container.

- `compose.prod.yml`: This file is used for production deployments. It sets up the API service without hot reloading and with optimizations for a production environment.

## Running the Project

### Local Development

To run the project locally with hot reloading:

1. Ensure Docker and Docker Compose are installed on your system.
2. Open terminal and navigate to the project's directory where the compose.yml and compose.prod.yml files are located.
3. Run the following command:

```sh
# This command starts the services defined in compose.yml and compose.local.yml. The -d flag runs the containers in the background.
docker-compose -f compose.yml -f compose.local.yml up -d

# To stop the services, run:
docker-compose -f compose.yml -f compose.local.yml down
```

### Production Deployment

To run the project in a production environment:

1. Make sure Docker and Docker Compose are installed on your production machine.
2. Open terminal and navigate to the project's directory where the compose.yml and compose.prod.yml files are located.
3. Run the following command:
```sh
# This will start the services as defined for the production setup. Again, the -d flag is used to run the containers in detached mode.
docker-compose -f compose.yml -f compose.prod.yml up -d

#To stop the services in production, use:
docker-compose -f compose.yml -f compose.prod.yml down
```

**Note:** It's important to keep your production environment variables and settings secure. Always check that your compose.prod.yml does not include sensitive data before pushing to any public repositories.

### Without Docker
To run the project without Docker, you will need to have:
- [.NET 8 SDK](https://dotnet.microsoft.com/download): The .NET SDK includes everything you need to build and run .NET Core applications.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads): The application requires a SQL Server instance to connect to for the database operations.

To run the project without Docker:

1. Restore the NuGet packages by running `dotnet restore` in the project directory.
2. Navigate to the project directory that contains the *.csproj file for the web API.
3. Ensure the connection strings in the appsettings file are correctly configured to point to your local SQL Server instance.
4. Run any pending migrations to create the database schema: `dotnet ef database update`
5. Start the application: `dotnet run`

>This command will compile and run the application. Your API should now be running on the configured local port (e.g., http://localhost:8080).

To stop the application, press `Ctrl+C` in the terminal window where it's running.