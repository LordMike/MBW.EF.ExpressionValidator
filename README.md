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

# Usage

Reference a package from above, depending on your target database, and then add it to your in-memory database context builder:

```csharp
var services = new ServiceCollection();

// When adding your DbContext to your Dependency Injection, 
// use the relevant extension methods to add validation.
services
        .AddDbContext<Context>(x => x.UseInMemoryDatabase(nameof(UseCaseAddDbContext))
            .AddSqliteExpressionValidation<Context>()
            .AddMysqlExpressionValidation<Context>()
            .AddSqlServerExpressionValidation<Context>());
        
var serviceProvider = services.BuildServiceProvider();

// Use the database like you normally would
var db = serviceProvider.GetService<Context>();

// A query like this, would work - and will therefore also work in actual Mysql, Mssql and Sqlite databases.
db.Blogs.Where(s => s.Id == 4).ToList();

// A query like this could work in the in memory database, but not in actual databases
// The expression validation will catch this, and throw an exception
db.Blogs.Where(s => s.Title.ToCharArray().Length == 2).ToList();
```
