# Objetivo

Apresentar e explicar todos os conceitos, técnicas e abordagens essenciais para o desenvolvimento de aplicações utilizando ASP.NET MVC.

# Padrão MVC

- Model;
- View;
- Controller.

- Padrão arquitetural;
    - Complementa um estilo arquitetural;
        - Ex: aplicação em camadas que adota o uso do MVC;
- Separação de responsabilidades;
- Criado (descrito) em 1976 pela Xerox;
- Pode ser usado em várias plataformas.   

## Model

As models representam os **dados** da aplicação. Além disso, uma Model pode incluir a validação dos dados.
Trata-se da representação dos dados do mundo real qua pode incluir validações e regras de negócio.

## Views

As views são as páginas do site, responsáveis pela navegação, design, UX. Utilizamos HTML, CSS e JS.
No MVC do .NET, temos o componente especial, o Razor. Ele é responsável por montar e entregar a view pronta para exibição.
O Razor permite funcionalidades dinâmicas dentro do HTML.

## Controller

Atua como intermediária entre a Model e a View. É a controller qu response às ações do usuário, como cliques em botões em links;

# Acompanhando as novidades

https://github.com/dotnet/aspnetcore/issues

# Estrutura projeto ASP NET Core MVC

- Pasta Properties
    - Contém o launchSettins.json, que permite configurar como rodar a aplicação;

- Pasta wwwroot
    - Contém os arquivos estáticos;

- Pasta controllers
    - Armazena as controllers das views

- Pasta models
    - Armazena os modelos usados na aplicação

- Pasta views
    - Armazena as páginas da aplicação;
    - O nome da view sempre segue o nome da controller;
        - Se temos HomeController, então dentro de views teremos a pasta Home;
    - Pasta Shared
        - Arquivos compartilhados;
            - Partials;

# Controllers

- **Controller** é a classe base do MVC;

## Rotas

Estruturas de navegação personalizadas para que a URL da aplicação possua determinado padrão e atenda às necessidades de passagem de parâmetros.
**A rota explica qual controller invocar, qual o método chamar e quais parâmetros passar.**

# Verbos HTTP

Mais usados no MVC:
- GET;
    - Pede (request) uma informação ao servidor;
    - É feito através da URL

- POST;
    - Envia informações ao servidor (formulários);

Contudo, temos também:

- Put;
    - Similar ao Post, mas é usado para atualizar informações existentes.
- Delete
    - Solicita exclusão de uma informação no servidor através da URL indicada.

# Action Result

No ASP.NET MVC um Action Result é o tipo de retorno da action da controller, é utilizada a interface IActionResult que pode retornar alguns tipos de resultados:

- JsonResult;
- ViewResult;
- RedirectResult;
- OkResult;
- ...;


# Attribute Routes

Rota por atributos é uma maneira alternativa de trabalhar com rotas, são muito mais flexíveis e fáceis de personalizar.
Estas rotas valem paenas a Controller que foi implementada.
São data annotations

```
[Route("alunos/novo-aluno")]
```

```
[HttpPost("alunos/novo-aluno")]
```

# Usando rotas

Precisa configurar no Program.cs o `app.UseRouting();`.


As controllers podem ter rotas definidas via Data Annotation. Ex:
`
[Route("minha-conta")]
public class TesteController : Controller {}
`
Obs: uma controller pode ter mais de uma rota definida. Ex:

`
[Route("minha-conta")]
[Route("gestao-da-conta")]
public class TesteController : Controller {}
`
Com as duas rotas acima, podemos acessar a controller através do `minha-conta` e `gestao-da-conta`.

Os endpoints dentro da controller podem receber configurações de rotas:

```
[Route("gestao-da-conta")]
public class TesteController : Controller {
    [Route("minha-conta/{id:int}")]
    public ActionResult Index(){}    
}
```

Outra forma

```
[Route("gestao-da-conta")]
public class TesteController : Controller {
    [HttpGet("minha-conta/{id:int}")]
    public ActionResult Details(int id){}    
}
```

# Recebendo parâmetros via body

```
[HttpPost("novo")]
public ActionResult Create([Bind("Id,Nome,Email")] Aluno aluno);
```

```
[HttpPost("novo")]
public ActionResult Create([FromForm] Aluno aluno);
```

```
[HttpPost("novo")]
public ActionResult Create([FromBody] Aluno aluno);
```

    


