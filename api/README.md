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

2. Open your browser and navigate to `https://localhost:5000/swagger` to view the Swagger API documentation.

### Usage

- Use the Swagger UI to explore and test the API endpoints.
- Use a tool like Postman to interact with the API.

### Development

- To enable hot-reloading during development, use:
  ```sh
  dotnet watch run
  ```

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

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.

### License

This project is licensed under the MIT License.
