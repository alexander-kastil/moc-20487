# Prerequisites & Setup

## .NET Core Setup

Net Core 2.2 SDK

https://dotnet.microsoft.com/download

Windows Hosting Bundle

https://www.microsoft.com/net/download/thank-you/dotnet-runtime-2.1.0-windows-hosting-bundle-installer

## SQL Server Setup

SQL Standard or Enterprise to be installed locally. If you want to use SQL Express change connection string in appsettings.json.

Don't forget to create SQL Login "angular" with pwd "angular". Add to role Sysadmin.

# Debugging & Setup

## Start VoucherService

Open to the folder where \*.csproj is located and run:

`dotnet restore` and then  
`dotnet run`

## Debugging .NET Core

```
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/bin/Debug/netcoreapp2.0/Vouchers.dll",
      "args": [],
      "cwd": "${workspaceFolder}",
      "stopAtEntry": false,
      "internalConsoleOptions": "openOnSessionStart",
      "launchBrowser": {
        "enabled": true,
        "args": "${auto-detect-url}",
        "windows": {
          "command": "cmd.exe",
          "args": "/C start ${auto-detect-url}"
        },
        "osx": {
          "command": "open"
        },
        "linux": {
          "command": "xdg-open"
        }
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "sourceFileMap": {
        "/Views": "${workspaceFolder}/Views"
      }
    },
    {
      "name": ".NET Core Attach",
      "type": "coreclr",
      "request": "attach",
      "processId": "${command:pickProcess}"
    }
  ]
}
```

## SSL

Add a trust for the Test Cert: `dotnet dev-certs https --trust`
