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

   ```
   cd api
   ```

2. Restore the dependencies:

   ```
   dotnet restore
   ```

3. Update the database:

   ```
   dotnet ef database update
   ```

4. Run the API:

   ```
   dotnet run
   ```

### Setting Up the Frontend

1. Navigate to the

frontend

directory:

    ```
    cd frontend
    ```

2. Install the dependencies:

   ```
   npm install
   ```

3. Run the development server:

   ```
   npm run dev
   ```

## Running Tests

### API Tests

1. Navigate to the

api.Tests

directory:

    ```
    cd api.Tests
    ```

2. Run the tests:

   ```
   dotnet test
   ```

### Frontend Tests

1. Navigate to the

frontend

directory:

    ```
    cd frontend
    ```

2. Run the tests:

   ```
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

# Songs API

An API for managing songs, built with ASP.NET Core.

## Features

- User authentication with JWT
- Role-based authorization
- Song management
- Swagger API documentation
- CORS support for React applications

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQLite](https://www.sqlite.org/download.html)

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/yourusername/SongsAPI.git
   cd SongsAPI
   ```

2. Install dependencies:

   ```sh
   dotnet restore
   ```

3. Update the connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Data Source=songs.db"
   }
   ```

4. Add JWT settings in `appsettings.json`:
   ```json
   "JwtSettings": {
       "Issuer": "your-issuer",
       "Audience": "your-audience",
       "SecretKey": "your-secret-key"
   }
   ```

### Running the API

1. Build and run the application:

   ```sh
   dotnet run
   ```

2. Open your browser and navigate to `https://localhost:5001/swagger` to view the Swagger API documentation.

### Usage

- Use the Swagger UI to explore and test the API endpoints.
- Use a tool like Postman to interact with the API.

### Endpoints

#### Authentication

- `POST /api/auth/login` - Authenticate a user and return a JWT token.
- `POST /api/auth/register` - Register a new user.

#### Songs

- `GET /api/songs` - Get a list of all songs.
- `GET /api/songs/{id}` - Get details of a specific song by ID.
- `POST /api/songs` - Add a new song.
- `PUT /api/songs/{id}` - Update an existing song by ID.
- `DELETE /api/songs/{id}` - Delete a song by ID.

#### Users

- `GET /api/users` - Get a list of all users.
- `GET /api/users/{id}` - Get details of a specific user by ID.
- `PUT /api/users/{id}` - Update an existing user by ID.
- `DELETE /api/users/{id}` - Delete a user by ID.

#### Roles

- `GET /api/roles` - Get a list of all roles.
- `POST /api/roles` - Create a new role.
- `DELETE /api/roles/{name}` - Delete a role by name.

### Development

- To enable hot-reloading during development, use:
  ```sh
  dotnet watch run
  ```

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.

### License

This project is licensed under the MIT License.
