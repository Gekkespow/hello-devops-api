# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj first (for faster caching)
COPY *.csproj ./

# Restore NuGet packages inside container
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish app to /app
RUN dotnet publish -c Release -o /app

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app .

# Make API listen on all network interfaces
ENV ASPNETCORE_URLS=http://+:80

# Run the app
ENTRYPOINT ["dotnet", "HelloDevOps.Api.dll"]
