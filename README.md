# MBW.EF.ExpressionValidator [![Generic Build](https://github.com/LordMike/MBW.EF.ExpressionValidator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/LordMike/MBW.EF.ExpressionValidator/actions/workflows/dotnet.yml) [![Nuget](https://img.shields.io/nuget/v/MBW.EF.ExpressionValidator)](https://www.nuget.org/packages/MBW.EF.ExpressionValidator)

This project aims to help developers avoid a pesky issue with EntityFramework: untranslateable queries. Whenever a developer uses the In-Memory database from Microsoft for local development, or any other provider for that matter, there is a risk that the query they're making can translate correctly to SQL for the current provider, but won't be able to for other providers. A good example is exactly with the in memory database, as it purely exists in memory and runs .NET code to produce results.

Using this project, it is possible to validate that all queries made can succesfully translate into queries for other databases.

# Features

* Plugins to translate queries to Mysql, Sqlite and Sql Server
* Easy to use API to plug in validation to a database context

# Nuget packages

| Name | Nuget | Note |
|---|---|---|
| MBW.EF.ExpressionValidator | [![Nuget](https://img.shields.io/nuget/v/MBW.EF.ExpressionValidator)](https://www.nuget.org/packages/MBW.EF.ExpressionValidator/) | Core functionality |
| MBW.EF.ExpressionValidator.Sqlite | [![Nuget](https://img.shields.io/nuget/v/MBW.EF.ExpressionValidator.Sqlite)](https://www.nuget.org/packages/MBW.EF.ExpressionValidator.Sqlite/) | Addon using `MBW.EF.ExpressionValidator.Sqlite` |
| MBW.EF.ExpressionValidator.SqlServer | [![Nuget](https://img.shields.io/nuget/v/MBW.EF.ExpressionValidator.SqlServer)](https://www.nuget.org/packages/MBW.EF.ExpressionValidator.SqlServer/) | Addon using `Microsoft.EntityFrameworkCore.SqlServer` |
| MBW.EF.ExpressionValidator.PomeloMysql | [![Nuget](https://img.shields.io/nuget/v/MBW.EF.ExpressionValidator.PomeloMysql)](https://www.nuget.org/packages/MBW.EF.ExpressionValidator.PomeloMysql/) | Addon using `Pomelo.EntityFrameworkCore.MySql` |
