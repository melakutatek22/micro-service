# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy project files
COPY notification-service/notification-service.csproj ./notification-service/
COPY libs/SharedLibrary.csproj ./libs/


# Restore dependencies
WORKDIR /src/notification-service
RUN dotnet restore

# Copy full source
COPY auth-service/. ./
COPY libs ../libs               
# Publish
RUN dotnet publish notification-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "notification-service.dll"]
