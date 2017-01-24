# FSL.RepositoryPatternWithDapperAndMongoDB

**Repository Patter with Dapper and MongoDB**

The gol is to show how to use **Repository Pattern** to access multiple databases. Our business class does not need to know where is the database or wich one is. Finally, we don't need to perform changes to our business class when changing the data class.

![enter image description here](https://fabiosilvalima.net/wp-content/uploads/2017/01/fabiosilvalima-repository-pattern-com-dapper-sql-e-mongodb.jpg)

I will  show an implementation of data acces using **SQL Server** with **Dapper** and another one with **MongoDB** ASP.NET oficial driver.

> **LIVE DEMO:**
> 
> http://codefinal.com/FSL.RepositoryPatternWithDapperAndMongoDB/

> **FULL ARTICLE:**
>
> English: https://fabiosilvalima.net/en/repository-pattern-dapper-sql-mongodb/
>
> Português: https://fabiosilvalima.net/repository-pattern-dapper-sql-mongodb/

---

What is in the source code?
---

#### <i class="icon-file"></i> FSL.RepositoryPatternWithDapperAndMongoDB

- Visual Studio solution file;
- StackExchange **Dapper** library;
- .NET **MongoDB** oficial driver;
- **MVC 5.2.3** and **Ninject** for **Dependency Injection**;
- Classes for our solution; 

> **Remarks:**

> - If you want to run that application you will need install **MongoDB** client and server in your machine. Download the installer [here][1]. You can configure a remote connection to **MongoDB** or **SQL Server**.
> - I use **Dapper** to SQL Server and **MongoDB** just as exemplification of Repository Pattern but you can create your own implementation for others data access libraries like **Entity Framework**.
> - I do not show how to install **MongoDB**. Basically you will install and run database service using commandline. There is a lot of client tools to connect and manage **MongoDB** database. I suggest you install **Robomongo**, download installer [here][4].

---

What is the goal?
---

I have a business logic that calculates something with some taxes/factors that are stored in database. Does not matter where the data are, just go get it e return them.

```sequence
Rule->Repository: Business calls repository
Note right of Repository: Repository Pattern
Repository-->Rule: Repository returns data
```

**Assumptions:**
- Does not matter where the data are;
- I must perform a little change to get the data in another database.
- I want to configure **Dependency Injection** to handle **Repository Pattern**.


Explaining...
---

- The SQL Server table name is **Taxas**. 
- The MongoDB collection name is **Taxas**.
- The connection ID of MongoDB is **localhost**. It's located at:

**Repository/MongoDBRepository.cs**
```csharp
public MongoDBRepository()
   : this("localhost")
{

}
```

- The SQL Server connection ID is  **sqlserver** and connection string is on web.config. It's located at:

**Repository/SqlServerRepository.cs**
```csharp
public SqlServerRepository()
	 : this("sqlserver")
{

}
```

- I configured **Dependency Injection** to Dapper Sql Server. It's located at:

**App_Start/NinjectWebCommon.cs**
```csharp
private static void RegisterServices(IKernel kernel)
{
	kernel.Bind<ITaxaRepository>().To<DapperTaxaRepository>();
}   
```

- The SQL Server schema database and data are located at **DatabaseScripts/SqlServer.sql**.
- The main code is located at HomeController and Action Index. 

```csharp
public async Task<ActionResult> Index()
{
    // Service handle the repository internally.
    var calculoA = TaxaService.CalcularTaxa(1);

    // Passing a DI repository to service
    var calculoB = TaxaService.CalcularTaxa(1, _repository);

    // Passing explicitally MongoDB Repository to service
    var calculoC = TaxaService.CalcularTaxa(1, new MongoDBTaxaRepository());

    await Task.WhenAll(calculoA, calculoB, calculoC);
            
    return View();
}
```

----------

References:
---

- StackExchange **Dapper** download e tutoriais [here][1];
- **MongoDB** download e tutoriais [here][2];
- **Ninject** tutoriais [here][3];
- **Robomongo** download e tutoriais [here][4];

Licence:
---

- [Licence MIT][4]


  [1]: https://github.com/StackExchange/dapper-dot-net
  [2]: https://www.mongodb.com/
  [3]: http://www.ninject.org/
  [4]: https://robomongo.org/
