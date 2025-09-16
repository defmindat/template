# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RiderProjects/EatWise/Directory.Build.props", "RiderProjects/EatWise/"]
COPY ["RiderProjects/EatWise/EatWise.API/EatWise.API.csproj", "RiderProjects/EatWise/EatWise.API/"]
COPY ["RiderProjects/EatWise/EatWise.Harvester.Application/EatWise.Harvester.Application.csproj", "RiderProjects/EatWise/EatWise.Harvester.Application/"]
COPY ["RiderProjects/EatWise/EatWise.Harvester.Domain/EatWise.Harvester.Domain.csproj", "RiderProjects/EatWise/EatWise.Harvester.Domain/"]
COPY ["RiderProjects/EatWise/EatWise.Common.Domain/EatWise.Common.Domain.csproj", "RiderProjects/EatWise/EatWise.Common.Domain/"]
COPY ["RiderProjects/EatWise/EatWise.Harvester.Infrastructure/EatWise.Harvester.Infrastructure.csproj", "RiderProjects/EatWise/EatWise.Harvester.Infrastructure/"]
COPY ["RiderProjects/EatWise/EatWise.Common.Infrastructure/EatWise.Common.Infrastructure.csproj", "RiderProjects/EatWise/EatWise.Common.Infrastructure/"]
COPY ["RiderProjects/EatWise/EatWise.Common.Application/EatWise.Common.Application.csproj", "RiderProjects/EatWise/EatWise.Common.Application/"]
COPY ["RiderProjects/EatWise/EatWise.Harvester.Presentation/EatWise.Harvester.Presentation.csproj", "RiderProjects/EatWise/EatWise.Harvester.Presentation/"]
RUN dotnet restore "./RiderProjects/EatWise/EatWise.API/EatWise.API.csproj"
COPY . .
WORKDIR "/src/RiderProjects/EatWise/EatWise.API"
RUN dotnet build "./EatWise.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EatWise.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EatWise.API.dll"]