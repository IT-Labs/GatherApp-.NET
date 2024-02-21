FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./GatherAppAPI/GatherApp.API.csproj", "GatherAppAPI/"]
COPY ["./GatherApp.Contracts/GatherApp.Contracts.csproj", "GatherApp.Contracts/"]
COPY ["./GatherApp.Services/GatherApp.Services.csproj", "GatherApp.Services/"]
COPY ["./GatherApp.Repositories/GatherApp.Repositories.csproj", "GatherApp.Repositories/"]
COPY ["./GatherApp.DataContext/GatherApp.DataContext.csproj", "GatherApp.DataContext/"]
RUN dotnet restore "./GatherAppAPI/GatherApp.API.csproj"
COPY . .
WORKDIR "/src/GatherAppAPI"
RUN dotnet build "./GatherApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GatherApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["./GatherAppAPI/appsettings.Development.json", "."]
ENTRYPOINT ["dotnet", "GatherApp.API.dll"]