# :moneybag: Finances Management System

![CI](https://github.com/gpreviatti/FinancesManagementSystem/actions/workflows/dotnet.yml/badge.svg) ![Sonar](https://github.com/gpreviatti/FinancesManagementSystem/actions/workflows/sonar.yml/badge.svg)


This is a simple software to management your finances, I tried to use concepts like **Solid, Clean Code and Domain Driven Design** to define the architecture and implement. If you have any doubt or suggestion let me know or open an issue on project repository.  



## :computer: Technologies

- .Net Core (5.0.4)

- Entity Framework Core

- Local Db (MSSQL server based)

- Razor Pages

- XUnit

- AutoMapper

- Swagger

  

## :runner: How  to run

Clone the repository

Than run the following commands

`dotnet build`

`cd src/Infrastructure/Data`

`dotnet ef database update`

`dotnet run`
