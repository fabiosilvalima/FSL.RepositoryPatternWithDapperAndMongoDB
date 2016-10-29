# FSL.RepositoryPatternWithDapperAndMongoDB

**Repository Patter with Dapper and MongoDB**

O objetivo é mostrar como usar o **Repository Pattern** para acessar múltiplos banco de dados. Na verdade a sua regra de negócio não precisa saber onde estão armazenados os dados e a ideia do **Repository Pattern** é alternar entre os banco de dados sem mudar todo o seu código fonte. 

Aqui eu mostro uma implementação de acesso ao **SQL Server** usando **Dapper** e uma outra usando o **MongoDB**.

---

O que há no código fonte?
---

#### <i class="icon-file"></i> FSL.RepositoryPatternWithDapperAndMongoDB

- Solution do Visual Studio para facilitar;
- Biblioteca **Dapper** do StackExchange;
- Driver oficial .NET para acesso ao **MongoDB**;
- **MVC 5.2.3** e **Ninject** para **Dependency Injection**;
- Classes que compõem a solução; 

> **Observação:**

> - Se você quiser rodar a aplicação, você talvez precise instalar o **MongoDB** na sua máquina. Faça download dele [aqui][1]. Você pode também informar uma conexão remota ao **MongoDB** se preferir. A mesma coisa server para o **SQL Server**.
> - O uso de **Dapper** e **MongoDB** é apenas demonstrativo, porém você pode criar a sua solução com outro banco de dados ou outras bibliotecas como **Entity Framework** usando a mesma lógica utilizada aqui.
> - Eu não explico como instalar e configurar o **MongoDB**, mas basicamente instale e execute o serviço do banco de dados (via commandline). Tem uma ferramenta legal para gerenciar o banco de dados, criar collections e manipular dados que é **Robomongo**, faça download dela [aqui][4]

---

Qual a proposta da solução?
---

Eu tenho uma regra de negócio que busca algumas taxas em repositório de forma que eu tenha que fazer alguns cálculos. Não importa onde esses dados estão armazenados, simplesmente vá buscar esses dados e me retorne.

```sequence
Regra->Repositório: Regra de negócio solicita dados
Note right of Repositório: Repository Pattern
Repositório-->Regra: Repositório retorna os dados
```

**PREMISSAS:**
- Eu, regra de negócios não quero saber onde estão armazenados os dados;
- Se eu quiser mudar onde os dados estão armazenados tenho que modificar o mínimo possível o meu código fonte, mas não posso mudar a minha regra de negócio.
- Eu quero configurar **Dependency Injection** para controlar o **Repository Pattern**.
- Eu quero ser possível forçar a chamada de um ou outro repositório (apenas exemplificativo).


Explicando...
---

- A tabela no SQL Server chama-se **Taxas**. 
- A collection no MongoDB chama-se **Taxas**.
- O ID da conexão com o MongoDB é **localhost**. Onde está:

**Repository/MongoDBRepository.cs**
```csharp
public MongoDBRepository()
   : this("localhost")
{

}
```

- O ID da conexão com o SQL Server é **sqlserver** e a string de conexão está no web.config. Onde está:

**Repository/SqlServerRepository.cs**
```csharp
public SqlServerRepository()
	 : this("sqlserver")
{

}
```

- Para **Dependency Injection** foi configurado a implementação do Repositório usando Dapper. Onde está:

**App_Start/NinjectWebCommon.cs**
```csharp
private static void RegisterServices(IKernel kernel)
{
	kernel.Bind<ITaxaRepository>().To<DapperTaxaRepository>();
}   
```

- O script para criação da tabela no SQL e carregando dos dados está em **DatabaseScripts/SqlServer.sql**.
- Tudo começa pela HomeController e Action Index. Não há nada em tela.

```csharp
public async Task<ActionResult> Index()
{
	// Exemplos de chamadas paralelas usando diferentes repositorios

    // Servico se vira para achar o repositorio
    var calculoA = TaxaService.CalcularTaxa(1);

    // Servico recebe o repositorio via DI
    var calculoB = TaxaService.CalcularTaxa(1, _repository);

    // Servico recebe o repositorio especifico do mongodb
    var calculoC = TaxaService.CalcularTaxa(1, new MongoDBTaxaRepository());

    await Task.WhenAll(calculoA, calculoB, calculoC);
            
    return View();
}
```

----------

Referências:
---

- StackExchange **Dapper** download e tutoriais [aqui][1];
- **MongoDB** download e tutoriais [aqui][2];
- **Ninject** tutoriais [aqui][3];
- **Robomongo** download e tutoriais [aqui][4];

Lincença:
---

- [Licença MIT][4]


  [1]: https://github.com/StackExchange/dapper-dot-net
  [2]: https://www.mongodb.com/
  [3]: http://www.ninject.org/
  [4]: https://robomongo.org/
