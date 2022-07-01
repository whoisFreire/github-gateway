FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.11 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 AS build
WORKDIR /src
COPY ["githubApi.Facades/githubApi.Facades.csproj", "githubApi.Facades/"]
COPY ["githubApi.Models/githubApi.Models.csproj", "githubApi.Models/"]
COPY ["githubApi.Services/githubApi.Services.csproj", "githubApi.Services/"]
COPY ["githubApi/githubApi.csproj", "githubApi/"]

RUN dotnet restore "githubApi/githubApi.csproj"
COPY . .
WORKDIR "/src/githubApi"
RUN dotnet build "githubApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "githubApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "githubApi.dll"]