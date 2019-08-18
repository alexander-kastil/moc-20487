FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49856
EXPOSE 44360

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY VouchersCore/VouchersCore.csproj VouchersCore/
RUN dotnet restore VouchersCore/VouchersCore.csproj
COPY . .
WORKDIR /src/VouchersCore
RUN dotnet build VouchersCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish VouchersCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vouchers.dll"]