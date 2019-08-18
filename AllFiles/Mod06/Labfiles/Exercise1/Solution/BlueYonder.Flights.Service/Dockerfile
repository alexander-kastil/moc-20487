FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "BlueYonder.Flights.Service.dll"]


# FROM microsoft/dotnet:2.1-sdk
# WORKDIR /app

# # copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore

# # copy and build everything else
# COPY . ./
# RUN dotnet publish -c Release -o out
# ENTRYPOINT ["dotnet", "BlueYonder.Flights.Service.dll"]
# EXPOSE 8080/tcp
# ENV ASPNETCORE_URLS https://*:5000
