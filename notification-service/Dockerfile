# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY notification-service/notification-service.csproj ./notification-service/
COPY libs/SharedLibrary.csproj ./libs/
COPY notification-service/nuget.config ./

# Set working directory to the project folder
WORKDIR /src/notification-service

# Clear NuGet caches
RUN dotnet nuget locals all --clear

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY notification-service/. ./
COPY libs ../libs

# Publish the application
RUN dotnet publish notification-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "notification-service.dll"]
