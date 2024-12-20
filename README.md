# SongsAPI

SongsAPI is a web API for managing songs, lessons, and students. It is built with ASP.NET Core and Entity Framework Core.

## Project Structure

# SongsAPI

SongsAPI is a web API for managing songs, lessons, and students. It is built with ASP.NET Core and Entity Framework Core.

## Project Structure

SongsAPI/
├── .github/
│ └── workflows/
├── api/
│ ├── Controllers/
│ ├── Data/
│ ├── Migrations/
│ ├── Models/
│ ├── Properties/
│ ├── Services/
│ ├── appsettings.json
│ ├── Program.cs
│ ├── SongsAPI.csproj
│ ├── SongsAPI.sln
│ └── songs_rows.csv
├──api.Tests/
│ ├── Controllers/
│ ├── Services/
│ ├── GlobalUsings.cs
│ ├── TestBase.cs
│ ├── UnitTest1.cs
│ └── api.Tests.csproj
├── frontend/
│ ├── public/
│ ├── src/
│ ├── .gitignore
│ ├── eslint.config.js
│ ├── index.html
│ ├── package.json
│ ├── postcss.config.js
│ ├── README.md
│ ├── tailwind.config.js
│ ├── tsconfig.app.json
│ └── tsconfig.json
├── .gitignore
└── workflows/

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for the frontend)

### Setting Up the API

1. Navigate to the `api` directory:

   ```sh
   cd api
   ```

2. Restore the dependencies:

   ```sh
   dotnet restore
   ```

3. Update the database:

   ```sh
   dotnet ef database update
   ```

4. Run the API:

   ```sh
   dotnet run
   ```

### Setting Up the Frontend

1. Navigate to the

frontend

directory:

    ```sh
    cd frontend
    ```

2. Install the dependencies:

   ```sh
   npm install
   ```

3. Run the development server:

   ```sh
   npm run dev
   ```

## Running Tests

### API Tests

1. Navigate to the

api.Tests

directory:

    ```sh
    cd api.Tests
    ```

2. Run the tests:

   ```sh
   dotnet test
   ```

### Frontend Tests

1. Navigate to the

frontend

directory:

    ```sh
    cd frontend
    ```

2. Run the tests:

   ```sh
   npm run test
   ```

## Project Structure

- **api**: Contains the ASP.NET Core API project.
- **api.Tests**: Contains the test project for the API.
- **frontend**: Contains the React frontend project.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.

````

## Installation

Instructions on how to install and set up the project.

## Usage

Examples of how to use the project.

## Contributing

Guidelines for contributing to the project.

## License

Information about the project's license.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for the frontend)

### Setting Up the API

1. Navigate to the `api` directory:

   ```sh
   cd api
   ```

2. Restore the dependencies:

   ```sh
   dotnet restore
   ```

3. Update the database:

   ```sh
   dotnet ef database update
   ```

4. Run the API:

   ```sh
   dotnet run
   ```

### Setting Up the Frontend

1. Navigate to the [`frontend`](frontend) directory:

   ```sh
   cd frontend
   ```

2. Install the dependencies:

   ```sh
   npm install
   ```

3. Run the development server:

   ```sh
   npm run dev
   ```

## Running Tests

### API Tests

1. Navigate to the [`api.Tests`](api.Tests) directory:

   ```sh
   cd api.Tests
   ```

2. Run the tests:

   ```sh
   dotnet test
   ```

### Frontend Tests

1. Navigate to the [`frontend`](frontend) directory:

   ```sh
   cd frontend
   ```

2. Run the tests:

   ```sh
   npm run test
   ```

## Project Structure

- **api**: Contains the ASP.NET Core API project.
- **api.Tests**: Contains the test project for the API.
- **frontend**: Contains the React frontend project.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
````
