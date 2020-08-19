#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["HackerNews.Api/HackerNews.Api.csproj", "HackerNews.Api/"]
RUN dotnet restore "HackerNews.Api/HackerNews.Api.csproj"
COPY . .
WORKDIR "/src/HackerNews.Api"
RUN dotnet build "HackerNews.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNews.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNews.Api.dll"]