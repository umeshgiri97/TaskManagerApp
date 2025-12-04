# ---------------------------------------------------------
# STAGE 1: Build the application
# ---------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY TaskManagerApp.csproj .
RUN dotnet restore

# Copy all remaining source code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o /app/publish


# ---------------------------------------------------------
# STAGE 2: Run the application
# ---------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Render requires apps to listen on port 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

# Run your app
ENTRYPOINT ["dotnet", "TaskManagerApp.dll"]
