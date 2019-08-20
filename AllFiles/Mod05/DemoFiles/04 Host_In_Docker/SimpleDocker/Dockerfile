# Multistage Build

# Runtime Image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

# Build Image
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["SimpleDocker.csproj", "./"]
RUN dotnet restore "./SimpleDocker.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "SimpleDocker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SimpleDocker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SimpleDocker.dll"]
