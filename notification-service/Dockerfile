# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

COPY libs/SharedLibrary.csproj ./libs/
COPY notification-service.csproj ./
RUN dotnet restore notification-service.csproj

COPY ../libs ./libs
COPY . .

RUN dotnet publish notification-service.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "notification-service.dll"]
