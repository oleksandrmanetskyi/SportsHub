#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SportsHub/SportsHub.csproj", "SportsHub/"]
COPY ["SportsHub.Services/SportsHub.Services.csproj", "SportsHub.Services/"]
COPY ["SportsHub.DataAccess/SportsHub.DataAccess.csproj", "SportsHub.DataAccess/"]
RUN dotnet restore "SportsHub/SportsHub.csproj"
COPY . .
# WORKDIR "/src/SportsHub/."
RUN dotnet build "/src/SportsHub/SportsHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportsHub/SportsHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsHub.dll"]