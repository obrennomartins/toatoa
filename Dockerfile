FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY src/ToAToa.DataAccess/ToAToa.DataAccess.csproj src/ToAToa.DataAccess/
COPY src/ToAToa.Domain/ToAToa.Domain.csproj src/ToAToa.Domain/
COPY src/ToAToa.Presentation/ToAToa.Presentation.csproj src/ToAToa.Presentation/
RUN dotnet restore src/ToAToa.Presentation/ToAToa.Presentation.csproj

COPY . ./
RUN dotnet build "./src/ToAToa.Presentation/ToAToa.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
RUN dotnet publish "./src/ToAToa.Presentation/ToAToa.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToAToa.Presentation.dll"]
