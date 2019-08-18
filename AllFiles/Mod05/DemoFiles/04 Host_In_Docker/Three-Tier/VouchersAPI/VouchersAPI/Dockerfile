FROM microsoft/dotnet:2.2-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/Vouchers.dll"]
EXPOSE 8080/tcp
ENV ASPNETCORE_URLS https://*:5000

#Build Image
# docker build --rm -f "Dockerfile" -t vouchersapi .

#Start SQL Linux
# docker run -d --name sqllinux -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=TiTp4SQL@dmin' microsoft/mssql-server-linux

#Run Image including SQL
# docker run -d --rm -it -p 8080:5000 --link sqllinux:sqllinux vouchersapi