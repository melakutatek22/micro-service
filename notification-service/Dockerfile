# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy project files
COPY notification-service/notification-service.csproj ./notification-service/
COPY libs/SharedLibrary.csproj ./libs/


# Restore dependencies
WORKDIR /src/notification-service
# 3. Update your Dockerfile with these commands before dotnet restore:
RUN dotnet nuget add source https://www.myget.org/F/masstransit/api/v3/index.json -n massTransitPreview
RUN dotnet nuget locals all --clear

RUN dotnet restore

# Copy full source
COPY notification-service/. ./
COPY libs ../libs               
# Publish
RUN dotnet publish notification-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "notification-service.dll"]
