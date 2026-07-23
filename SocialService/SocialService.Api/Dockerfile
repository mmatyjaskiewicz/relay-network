FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["SocialService/SocialService.Api/SocialService.Api.csproj", "SocialService/SocialService.Api/"]
COPY ["SocialService/SocialService.Application/SocialService.Application.csproj", "SocialService/SocialService.Application/"]
COPY ["Shared/Shared.Contracts/Shared.Contracts.csproj", "Shared/Shared.Contracts/"]

RUN dotnet restore "SocialService/SocialService.Api/SocialService.Api.csproj"

COPY . .

WORKDIR /src/SocialService/SocialService.Api

RUN dotnet build "SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release

WORKDIR /src/SocialService/SocialService.Api

RUN dotnet publish "SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SocialService.Api.dll"]