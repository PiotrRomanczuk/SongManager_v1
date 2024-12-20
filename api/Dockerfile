# Use the official .NET image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file and project files
COPY *.sln ./
COPY api/*.csproj ./api/
COPY api.Tests/*.csproj ./api.Tests/

# Restore dependencies
RUN dotnet restore ./api/SongsAPI.csproj

# Copy the rest of the application
COPY . ./

# Build the application
RUN dotnet publish ./api/SongsAPI.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SongsAPI.dll"]