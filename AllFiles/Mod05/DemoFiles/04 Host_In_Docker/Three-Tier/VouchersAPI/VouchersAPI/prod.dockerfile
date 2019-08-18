FROM microsoft/microsoft/aspnetcore
LABEL author="Alexander Pajer"
ARG source=.
WORKDIR /app
EXPOSE 5000
COPY ${source} .
ENTRYPOINT dot netVouchers.dll
