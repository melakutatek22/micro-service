# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy project files
COPY reports-service/reports-service.csproj ./
COPY libs/SharedLibrary.csproj ./libs/

# Restore dependencies
RUN dotnet restore reports-service.csproj

# Copy full source
COPY reports-service/. ./
COPY libs/. ./libs/

# Publish
RUN dotnet publish reports-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "reports-service.dll"]
