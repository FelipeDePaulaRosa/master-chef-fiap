# Master Chef Fiap

This is a web application built using .NET 7.

### Requirements

- .NET 7 SDK

### Installation
1 - Clone this repository  
2 - Go to the root of the directory 

3 - Configure your connectionString in appsettings.Development.json  or create on docker the same instace with the following command:

#### SQL Server on Docker
```bash
docker run -p 1433:1433 -e "MSSQL_SA_PASSWORD=Sqlserver@123" -e "ACCEPT_EULA=Y" --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2017-latest
```

4 - Execute the following command in bash
```
dotnet build
dotnet run
```
In your browser, go to http://localhost:5036/swagger/index.html to access the API documentation.