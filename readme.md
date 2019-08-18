# Prerequisites

Download & Install [VSCode](https://code.visualstudio.com/download)

## Azure CLI

[Install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)

[Azure CLI Reference](https://docs.microsoft.com/en-us/cli/azure/reference-index?view=azure-cli-latest)

## Certificates

Enable Chrome to trust self-signed localhost certs:

```
chrome://flags/#allow-insecure-localhost
```

Certificate Troubleshooting

```
dotnet dev-certs https --clean
dotnet dev-certs https -t
```

## Running .NET Core

Download NuGet Packages

`dotnet restore`

Run Application

`dotnet run` | `dotnet watch run`

Run .Net Core that is available using IP-Address

```
dotnet run --urls http://0.0.0.0:5000 or dotnet run --urls https://0.0.0.0:5001
```
