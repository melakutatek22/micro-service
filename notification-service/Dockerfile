# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy project files
COPY notification-service/notification-service.csproj ./
COPY libs/SharedLibrary.csproj ./libs/

# Restore dependencies
RUN dotnet restore notification-service.csproj

# Copy full source
COPY notification-service/. ./
COPY libs/. ./libs/

# Publish
RUN dotnet publish notification-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "notification-service.dll"]
