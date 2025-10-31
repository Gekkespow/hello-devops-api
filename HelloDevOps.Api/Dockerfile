# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj first (for faster rebuilds)
COPY *.csproj ./

# Restore dependencies inside the container
RUN dotnet restore

# Copy everything else
COPY . .

# Publish the app
RUN dotnet publish -c Release -o /app

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "HelloDevOps.Api.dll"]
