# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

# Install curl for healthchecks 
USER root 
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/* 

USER $APP_UID
WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["CentralStore/CentralStore.csproj", "CentralStore/"]
RUN dotnet restore "./CentralStore/CentralStore.csproj"
COPY . .
WORKDIR "/src/CentralStore"
RUN dotnet build "./CentralStore.csproj" -c $BUILD_CONFIGURATION -o /app/build
#COPY CentralStore/bin/Debug/net10.0/*.pdb /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./CentralStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#COPY CentralStore/bin/Debug/net10.0/*.pdb /app/publish

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app

# Install curl for healthchecks
#USER root
#RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
#USER $APP_UID

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CentralStore.dll"]