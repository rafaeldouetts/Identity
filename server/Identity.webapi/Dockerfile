#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["server/Identity.webapi/Identity.webapi.csproj", "server/Identity.webapi/"]
COPY ["server/Identity.Blob/Identity.Blob.csproj", "server/Identity.Blob/"]
COPY ["server/Identity.domain/Identity.Domain.csproj", "server/Identity.domain/"]
COPY ["server/Identity.Infra/Identity.Infra.csproj", "server/Identity.Infra/"]
RUN dotnet restore "./server/Identity.webapi/Identity.webapi.csproj"
COPY . .
WORKDIR "/src/server/Identity.webapi"
RUN dotnet build "./Identity.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.webapi.dll"]