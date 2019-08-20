# Migrations

To manage Migrations & create the DB go to console:

```
dotnet ef migrations add MIGRATION-NAME
dotnet ef database update
```

# Remote Debugging

For device testing it might be helpful to enable remote access - by default the .Net Core API is only available using `http://localhost:PORT/` and is not listening to the ip of your dev machine.

```
dotnet run --urls http://0.0.0.0:5000 or dotnet run --urls https://0.0.0.0:5001
```
