# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Render uses $PORT — expose and bind it
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 80

ENTRYPOINT ["dotnet", "backend_API.dll"]
