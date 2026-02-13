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
        - Arquivos compartilhados e que podem ser acessados por qualquer controller.;
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


# Rotas

## Attribute Routes

Rota por atributos é uma maneira alternativa de trabalhar com rotas, são muito mais flexíveis e fáceis de personalizar.
Estas rotas valem paenas a Controller que foi implementada.
São data annotations

```
[Route("alunos/novo-aluno")]
```

```
[HttpPost("alunos/novo-aluno")]
```

## Usando rotas

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

## Recebendo parâmetros via body

```
[HttpPost("novo")]
public ActionResult Create([Bind("Id,Nome,Email")] Aluno aluno);
```
**O bind é util para especificar quais campos precisam ser enviados para a controller, evitando que seja enviado um dado que não precisa ser adicionado/alterado.**


```
[HttpPost("novo")]
public ActionResult Create([FromForm] Aluno aluno);
```

```
[HttpPost("novo")]
public ActionResult Create([FromBody] Aluno aluno);
```

## Roteamento avançado

### Ordem de configuração

Da mais complexa para a mais simples. O .NET vai testar uma por uma até consegui encontrar a respectiva rota. Logo, se a mais simples é a primeira, o .NET pode acabar utilizando ela em vez da sua customizada (complexa).

**Um uso para o exemplo abaixo é com idiomas.**

```
app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
```

### Transformadores de rota
É possível aplicar uma transformação no nome de uma rota.

- Criar uma classe que herde de `IOutboundParameterTransformer`;
- Implementar método `public string? TransformOutbound(object? value)`;
- Configurar em program para utilizar a classe:
```
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["ChaveRota"] = typeof(MinhaClasse);
});
```
- **Importante: A chave usada em ConstraintMap (ChaveRota) deve ser especifica nas configurações de rota. Ver exemplo abaixo: **
- Especificar as rotas que vão usar a configuração:
```
app.MapControllerRoute(
    name: "default",
    pattern: "{controller:ChaveRota=Home}/{action:ChaveRota=Index}/{id?}"
);
```
- **Importante: observe onde foi colocado a chave definida em ConstraintMap (ChaveRota). É preciso fazer essa configuração para o .NET aplicar o transformador.**


# Action Result na prática

IActionResult -> Interface;
ActionResult -> Implementação da interface.

**Recomenda-se utilizar a interface. A classe acaba sendo usada em situações mais complexas**.

## Especificando qual View usar

```
public ActionResult Index(){
    return View("NomeDaView");
}
```

Omitir o "NomeDaView" fará com que seja buscado a view com nome Index.

## Usando View com uma model


```
public ActionResult Index(){
    return View("NomeDaView", new {});
}
```

ou

```
public ActionResult Index(){
    return View(new {  });
}
```

# Models

Representa os objetos do mundo real.
No MVC um modelo pode ser um conjunto de informações de diversos objetos em um só, esse conceito
é chamado de DTO (Data Transfer Object) que são muito utilizados para diminuir o número de requisições
no servidor.

**Só é possível usar uma model por View**

## DataAnnotations

Recurso utilizado principalmente para especificar que tipo de dado e formato a propriedade da Model deve receber.
Também é utilizada para definir tamanho, padrão e obrigatoriedade de preenchimento.
Pode ser utilizada para mapeamento com o banco de dados.
Pode ser utilizado para trabalhar como validação nos formulários nas Razor Views.

### DataAnnotations para validação

Exemplos:

**Campo obrigatório**
```
[Required(ErrorMessage="O campo {0} é obrigatório")]
public string Nome {get; set;}
```

**Validando email**
```
[EmailAddress(ErrorMessage="O campo {0} está em formato inválido")]
public string Email {get; set;}
```

**Comparando campos**
```
[Compare("Email", ErrorMessage="Os e-maisl informados não são identicos")]
public string EmailConfirmacao {get; set;}
```

**Intervalo**
```
[Range(1, 5, "O campo {0} deve estar entre {1} e {2})]
public string Avaliacao {get; set;}
```

**Desconsiderando atributo para base de dados**
```
[NotMapped]
public string EmailConfirmacao {get; set;}
```


## ModelState

### TryValidateModel

Método que valida se um modelo é valido ou não

```
if(TryValidateModel(aluno)) {
    ...
}
```

# Views e Razor

## Razor Views

No MVC, o motor de renderização das Views chama-se Razor. Logo nós temos as Razor Views que são arquivos HTML mesclados com recursos dao Razor.
O mecanismos do Razor transforma as views em arquivos HTML Puros para a interpretação do Browser.
As Views se tornam fortemente tipadas, permitindo trabalhar com modelos dentro das views.
Razor trabalha no backend convertendo o código da view em HTML puro.

## Tag Helpers

Recursos do razor para geração de HTML
O objetivo é escrever menos código, assim o Razor que interage com os recursos do ASP.NET é capaz de gerar o HTML necessário para a View.

Tag Helpers se misturam muito bem com o HTML inclusive dentro de atributos.

```
    <form asp-action="Create">
    </form>
```

O exemplo acima faz com que o form submeta os dados para a Action Create.

Podemos fazer um comparativo com o Angular ou React, onde usamos tags customizadas.

### Outros Exemplos

Mostra o nome da propriedade (valor atribuido ao Annotation Display).
```
@Html.DisplayNameFor(m => m.Nome); @*Mostra o nome da propriedade*@
```

Mostra o valor de uma propriedade
```
@Html.DisplayFor(m => aluno.Nome)
``


Passando parâmetros de rotas via link (tag a)
```
<a asp-action="Detalhes" asp-route-id="@aluno.Id">
```
O que vem depois do "asp-route-" é o nome do parâmetro.

Dentro de "_Layout" há a seguinte configuração:
```
@await RenderSectionAsync("Scripts", required: false)
```

Essa configuração permite fazer isso:

```
@section Scripts {
    @{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }
}
```

Ou seja, posso criar um bloco (section) com o nome que foi configurado (scripts) e carregar partials, scripts, etc.

Demonstra os erros do modelo em cada campo
```
 <div asp-validation-summary="ModelOnly" class="text-danger">
 </div>
```

Demonstra um resumo de todos os erros que ocorreram
```
 <div asp-validation-summary="All" class="text-danger">
 </div>
```


## Views de configuração

Diretórios

- Views
    - Shared
        - _Layout.cshtml
    - Home
    - _ViewImports.cshtml
    - _ViewStart.cshtml

### _ViewStart Page

Define qual o layout padrão do site. A estrutura, por padrão, é nesse formato:

```
@ {
    Layout = "_Layout";
}
```
### _ViewImports Page

Realiza a importação (using) de classes que vamos estar utilizando em nosso projeto.

### _Layout.cshtml

Localizada dentro de Shared (pasta compartilhada).
O Layout pode ser alterado em cada View, bastando alterar o valor da variável Layout
```
    @{
        Layout = "";
    }
```

### Partial Views

São pedações de uma View que podem ser reutilizados em N Views, proporcionando mais reaproveitamento de código.
Similar aos componentes do React e Angular.

**As partials views dependem do modelo implementado na View principal, gerando certa limitação em seu uso.**

Podemos usa-la com AJAX para renderizar dinamicamente.

Partial views possuem underline no nome, ex: `_Layout.cshtml`.

**Partial views devem ficar em Shared.**

Invocando uma partial:

```
<partial name="_MenuLayout">
```

Invocando de forma assincrona

```
@await Html.PartialAsync("_Menulayout")
```

### View Components

Recurso do ASP.NET + Razor, e é um poderoso aliado para desenvolvimento de componentes independentes das views.
- Podemos criar/utilizar um modelo especifico para o ViewComponent. As partials dependem do modelo no qual elas são invocadas.
- Possui um processamento próprio (server-side) independente da controller.
- Permite componentizar recursos de páginas como uma carrinho de compras, paginação, barra de pesquisa, etc.
- Devem ficar dentro uma pasta chamada `ViewComponents`.
- Toda ViewComponent é uma classe que herda de ViewComponent:

```
public class SaudacaoAlunosViewComponent : ViewComponent {
    public async Task<IViewComponentResult> InvokeAsync(){
        ...
        return View();
    }
}

```
O nome da classe deve ter ViewComponent
O processamento ocorre sem a necessidade de uma controller.

Devemos criar uma view. A view deve ficar dentro de Shared. A estrutura ficará:

- Shared
    - Components
        - SaudacaoAluno
            - Default.cshtml

`Default.cshtml`:
```
@model Aluno

<h1>Olá, @Model.Nome, seja bem-vindo(a)!</h1>
```

Invocando um ViewComponent:

```
@await Component.InvokeAsync("SaudacaoAluno")
```

Invocando no formato de tag

Em `_ViewImports.cshtml`:

```
@addTagHelper "*, PrimeiraApp"
```

Depois será possível invocar a ViewComponent:

```
<vc:saudacao-aluno />
```

É possível utilizar parâmetros nas ViewComponent:

```
public class SaudacaoAlunosViewComponent : ViewComponent {
    public async Task<IViewComponentResult> InvokeAsync(int idade){
        ...
        return View();
    }
}
```

```
    <vc:saudacao-aluno idade="18" />
```


# Entity Framework

- ORM (Object Relational Maping);
- Mapeia o mundo relacional aos objetos do C#;
- Depende do ADO.NET;


## Configurando o EF na aplicação

Precisamos instalar dois pacotes do EF:

- Microsoft.EntityFrameworkCore; (a versão deve ser compatível com a versão do .NET);
- Microsoft.EntityFrameworkCore.SqlServer; (a versão deve ser compatível com a versão do .NET);
    - Se estiver usando outro gerenciador de banco de dados, deve-se procurar o pacote correspondente ao SGDB utilizado.
- Microsoft.EntityFrameworkCore.Tools; (a versão deve ser compatível com a versão do .NET);
    - Permite usar commandos no Package Manager Console, como Add-Migration.

Depois, deve-se criar a classe que irá estender de DbContext:

```
public classe AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
}
```

Deve-se configurar o uso do context na Program.cs. Ex usando SqlServer:

```
builder.Services.AddDbContext<AppDbContext>(o => {
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```

Configurando uma entidade:

```
public classe AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    public DbSet<Aluno> Alunos {get; set;}
}
```






# Criando base de dados

https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#prerequisites

## Migration

É uma boa prática criar a migration com a versão na frente do nome. Ex: v01_Aluno
Há uma tabela que é gerada pelas migrations e que gerencia as versões das migrations.
Deve-se versionar as migrations

Uma forma de reverter uma migration é retornar para a versão anterior da migration que queremos defazer a ação.

# ASP.NET Identity

- Controle de usuários;
    - Login;
- Middleware de segurança que trabalha com Autenticação e Autorização de usuários e possui diversas funcionalidades.
- Consegue atender a todas as necessidades relacionadas a gerenciamento de acesso de usuários;
- Arquitetura bastante abstraída, sendo fácil estender e modificar comportamentos.


# Desenvolvendo uma aplicação funcional

- Criar um solution vazia;
- Criar uma pasta src na mesma raiz da soluction;



# Trabalhando com vários ambientes

No Razor, podemos utilizar a seguinte sintaxe para trabalhar com vários ambientes:

```
 @* Inclui recursos para o ambiente de desenvolvimento *@
 <environment include="Development">
 </environment>

 @* Exclui recursos para o ambiente de desenvolvimento *@
 <environment exclude="Development">
 </environment>
```

# Scaffold

Gera código prontos para ajudar na produtividade.

Não se esqueça: não faz sentido perder produtividade com algo que existe para te ajudar na produtividade.

# Manutenção de estado

Os itens abaixo permitem passar dados da Controller para a View. 

- ViewBag;
- ViewData;
- TempData

## ViewBag e ViewData

Fazem a mesma coisa. A diferença é o tipo de cada um. ViewBag permite atribuição usando notação ponto
e ViewData notação colchetes.

- Dados de curto tempo de duração;
- Duração de apenas um request;
- Após lidas perdem o conteúdo

## TempData

- Dura mais de um request;
- Assemelha-se a uma sessão (section);
    - Guardando os dados na memória do servidor;
    - Não é indicado.
- Após lidas perdem o conteúdo;
- Perduram enquanto não forem lidas.

# Autorização e autenticação

Podemos usar o DataAnnotation `Authorize` direto na controller. Para endpoints, podemos colocar exceções, como `AllowAnonymous`.



# Criando projeto sem template

- Criar solução em branco;
- Criar projeto em branco;
- Em Program.cs, adicionar a linha: `builder.Services.AddControllersWithViews(); // Configuração mais simples que o AddMVC`
- Ainda em Program.cs, acrescentar: 
```
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
```
## Configuração Frontend

- Crie a pasta `wwwroot`;
    - Dentro de wwwroot, crie: css, js, lib e images
- Acrescente em `program.cs`:
```
    // Permite olhar a pasta wwwroot
    app.UseStaticFiles();
```

### Adicionado lib (usando libman)

Clicar com o botão direito em cima de lib, ir em Add e depois vá na opção Add Client-Side Library.

### Criando _Layout

- Dentro de views, crie a pasta Shared;
- Crie o arquivo `_Layout.cshtml`;
- Em views, crie a _ViewStart.cshtml e especifique o Layout da seguinte forma;
```
@{
    Layout = "_Layout";
}
```


### Configurando Tag Helpers

- Crie o arquivo `_ViewImports.cshtml` em `Views` e acrescente o seguinte conteúdo:
```
@using SeuNamespace
@addTagHelpers *, Microsoft.AspNetCore.Mvc.TagHelpers
```

## Ferramentas de bundling e minification 

Bundling -> Transformar vários arquivos em um único arquivo. Dessa forma, o browser em vez de baixar vários arquivos, baixa apenas um, trazendo agilidade no carregamento do site.

Minification -> Remoção de excessos (espaço em branco, comentários, etc...) dos arquivos para reduzir o tamanho.

### Configurando no Visual Studio

- Baixar a extensão Bundler & Minifier (criado por Jason Moore) no Visual Studio.
    - `dotnet add package BuildBundlerMinifier` (Via linha de comando);
- Criar o arquivo `bundleconfig.json` na raiz do projeto;
- Exemplo de configuração que pode ser colocada no arquivo `bundleconfig.json`:
```
    [
  {
    "outputFileName": "wwwroot/css/site.min.css",
    "inputFiles": [
      "wwwroot/lib/bootstrap/css/bootstrap.css",
      "wwwroot/css/site.css"
    ]
  },
  {
    "outputFileName": "wwwroot/css/site.min.js",
    "inputFiles": [
      "wwwroot/js/site.js",
      "wwwroot/js/mat.js"
    ],
    "minify": {
      "enabled": true,
      "renameLocals": true
    },
    "sourceMap": false
  }
]
```
- Feito isso, basta clicar com o botão direito em cima de `bundleconfig.json`, procurar a opção `Bundler & Minifier` e clicar em `Update`.

Obs:  O `"renameLocals": true` altera os nomes das variáveis para nomes menores.
`"sourceMap": false` é para debug. Precisa ser true para debugar.

**Cuidado com variáveis com nomes iguais**;
**Não minificar bibliotecas externas. Foi utilizado no exemplo apenas para demonstração**.

#### Usando minificação apenas em prod

Utilizar o seguinte trecho:


```
<environment include="Development">
    <link rel="stylesheet" href="~/lib/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="~/css/site.css" />
</environment>

<environment exclude="Development">
    <link rel="stylesheet" href="~/css/site.min.css" />
</environment>
```

# Tag Helper customizado

- Crie uma classe que estenda de TagHelper;
```
 [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")] // Para qual elemento vou usar esse helper. * permite que seja usado em qualquer tag
 public class BotaoTagHelper : TagHelper
```
- Adicione o namespace em _ViewImports.cshtml. Ex: `@addTagHelper *,  Lab.MVC.AppSemTemplate``;

# Area

- Maneira de organizar uma aplicação MVC em grupos funcionais menores, cada um com seu próprio conjunto de Models, Views e Controllers.
- Nova estrutura MVC dentro da aplicação ASP.NET MVC;
- Tomar cuidado ao usar, pois pode gerar complexidade;
- Pode ser criado via scaffolding, através de `Commom -> MVC AREA`
    - Será criado uma pasta Areas, contendo toda estrutura MVC;
    -   Após rodar o comando, será necessário configurar a Area no program.cs, especificando o mapeamento da rota.

        - Ex: ```
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}"
                    );
           ```
        - Na controller, deve-se acrescentar `[Area("NomeDaArea")]` em cima da Controller.
 - É possível configurar um novo layout dentro das `Area` utilizando a configuração de Shared, _ViewImports e _ViewStart; 
 
 ## Alterando convenção Areas

 Para alterar a convenção da aplicação buscar/usar `Areas`, deve-se utilizar a seguinte configuração:
 ```
 builder.Services.Configure<RazorViewEngineOptions>(opt =>
{
    opt.AreaViewLocationFormats.Clear();
    opt.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
    opt.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
    opt.AreaViewLocationFormats.Add("/Modulos/Shared/{0}.cshtml");
});
 ```

 No exemplo acima, o .NET irá procurar por módulos em vez de `Areas`.

# Injeção de dependência (DI)

## Visão geral

- DI é um padrão de projeto que ajuda a reduzir o acoplamento de código em sua aplicação;
- No contexto do ASP.NET Core, DI é usada para adicionar serviços e gerenciar a vida útil dos objetos;
- ASP.NET Core possui um contêiner de injeção de dependência integrado;
    - O Contêiner é responsável por entregar os objetos instânciados;
- Para que um serviço seja injetado no código é necessário registrá-lo ao contêiner de DI.
`builder.Services.AddScope<ApplicationDbContext>`
- Para usar o serviço registrado é de costume injetar este serviço via construtor;

## Configurando DI

A configuração padrão do DI é feita em Programa.cs: `builder.Services.AddScoped<Interface, Classe>();`
Interface - contrato
Classe - classe que implementa a Interface

## Ciclo de vida do DI

- Transiente (transient)
    - Obtém uma nova instância do objeto a cada solicitação;
    - Todas as vezes que preciso pegar uma instância, o .NET gera uma nova;
- Scoped
    - Reutiliza a mesma instância do objeto durante todo o request (web);
- Singleton
    - Utiliza a mesma instância para toda aplicação;
        - Cuidado: usar para ações que não envolvem estado. Ex: serviço de envio de email.
        - Há o risco de dois ou mais usuários mexerem em uma mesma instância de um objeto;
    - O apontamento da instância só muda quando a aplicação é reiniciada;

## Outras maneiras de fazer DI

**De preferência para fazer DI no construtor**

- Utilizando annotation `FromServices` (não é aconselhável):
    ```
    [Route("Teste")]
    public string Teste([FromServices] MeuServico servico){
        ...
    }
    ```
- Utilizando DI na View (não é aconselhável)
    ```
        @inject MeuServico servico
    ```

## Acessando o contêiner de DI

- É feito através do `ServiceProvider`;

- `CreateScope`não faz referência ao ciclo de vida (scoped). No caso, faz referência ao escopo de serviços registrados;
```c#
using(var serviceScope = ServiceProvider.CreateScope()){
    var services = serviceScope.ServiceProvider;

    var service = services.GetRequiredService<IMeuServico>();
}
```
- É possível acessar os serviços antes da aplicação iniciar (program.cs). O `ServiceProvider` é obtido através do `app`;

```c#
using(var serviceScope = app.Services.CreateScope()){
    var services = serviceScope.ServiceProvider;
    
    var service = services.GetRequiredService<IMeuServico>();
}
```

# Segurança

- OWASP -> organização que cataloga brechas de segurança;

## Cross-Site Request Forgery

Mais informações sobre CSRF podem ser encotrados no arquivo sobre [CSRF](../../Segurança/CSRF.md)

### Configuração de token Anti-CSRF global no .NET

```
builder.Services.AddControllersWithViews(opts => {
    opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
```
### Configuração de token CSRF em endpoints no .NET

Basta adicionar o annotation `[ValidateAntiForgeryToken]` em cima do endpoint.

## HTTPS e HSTS

HSTS significa HTTP strict transport security.

O `app.UseHsts()` alocada um header que obriga o uso do HTTPs no domínio. Isso faz com que o browser seja obrigado a usar HTTPs. O browser nem tenta bater na rota com HTTP.
![alt text](AddHsts.png)

O `app.UseHttpsRedirection()` redireciona a rota para HTTPs se tentar acessar via HTTP

## Configurando o ASP.NET Identity

Pacotes necessários:

- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI

Usando na prática

`public class AppDbContext : IdentityDbContext {}`
O context deve herdar de `IdentityDbContext`.

Configurando

```
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<AppDbContext>();
...

app.UseAuthorization();
```

Será necessário rodar o `Add-Migration` e depois o `Update-Database`.

Para acessar as páginas do Identity é necessário incluir `app.MapRazorPages();`.

Da para sobrescrever algumas funções do Identity, como o layout, criando a seguinte estrutura:
`Area > Identity > Pages > _ViewStart.cshtml`.

O Identity possui Scaffolding das áreas, incluindo a de login. Com isso, é possível sobrescrever as views.

## Autenticação

Uma vez que o Identity foi configurado, é possível obter informações do usuário logado através do HttpContext:
`HttpContext.Identity.User`

## Autorização

A autorização pode ser feita através do DataAnnotation `Authorize` que pode ser colocado em cima do controller.

```
[Authorize]
public class AccountController : Controller
{
    public ActionResult Login()
    {
    }

    public ActionResult Logout()
    {
    }
}
```

Na configuração acima, todas as `Actions` da controller exigem que o usuário esteja logado. Contudo, é possível aplicar essa configuração para uma ou mais `Action`.

```
public class AccountController : Controller
{
   public ActionResult Login()
   {
   }

   [Authorize]
   public ActionResult Logout()
   {
   }
}
```

No exemplo acima, apenas usuários logados podem invocar o método `logout`.

Pode-se, também, aplicar uma configuração que exige autorização para todas as actions e liberar o acesso para algumas:
```c#
[Authorize]
public class AccountController : Controller
{
    [AllowAnonymous]
    public ActionResult Login()
    {
    }

    public ActionResult Logout()
    {
    }
}
```

**Obs: o annotation [Authorize] não pode ser aplicado em Razor pages.**

No exemplo acima, a configuração `[AllowAnonymous]` sobrescreve o `[Authorize]` para o método `Login` fazendo com que o método seja acessível por todos.

**Aplicar o `[AllowAnonymous]` a nível do controller fará com que as actions com `[Authorize]` sejam ignoradas.**

### Policies
Uma política consiste em um ou mais requerimentos. 
Políticas permitem agrupar regras, facilitando, por exemplo, liberar/barrar ações. 

Como configurar:
```c#
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast21", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(21)));
});
```

Uma vez configurada, pode-se usar da seguinte forma:

```c#
[Authorize(Policy = "AtLeast21")]
public class AtLeast21Controller : Controller
{
    public IActionResult Index() => View();
}
```

É possível aplicar políticas a nível da controller e das actions:

```
[Authorize(Policy = "AtLeast21")]
public class AtLeast21Controller2 : Controller
{
    [Authorize(Policy = "IdentificationValidated")]
    public IActionResult Index() => View();
}
```

**Todas as políticas precisam ser atendidas para o acesso ser liberado**

**Em Razor pages, políticas devem ser colocadas a nível de página (Page) e não a nível de handler.**

### Role
Acesso baseado em papel. Pode-se configurar o `Authorize` para liberar o acesso de acordo com o papel da pessoa no sistema.

Configuração base:
```c#
builder.Services.AddDefaultIdentity<IdentityUser>( ... )
    .AddRoles<IdentityRole>()
    ...
```

```
[Authorize(Roles = "Administrator")]
public class AdministrationController : Controller
{
    public IActionResult Index() =>
        Content("Administrator");
}
```

É possível configurar mais de uma role
```
[Authorize(Roles = "HRManager,Finance")]
public class SalaryController : Controller
{
    public IActionResult Payslip() =>
                    Content("HRManager || Finance");
}
```

No exemplo acima, o usuário deve ser `HRManager` ou `Finance`

```c#
[Authorize(Roles = "PowerUser")]
[Authorize(Roles = "ControlPanelUser")]
public class ControlPanelController : Controller
{
    public IActionResult Index() =>
        Content("PowerUser && ControlPanelUser");
}
```

No exemplo acima, o usuário deve ser `PowerUser` e `ControlPanelUser`.


```c#
[Authorize(Roles = "Administrator, PowerUser")]
public class ControlAllPanelController : Controller
{
    public IActionResult SetTime() =>
        Content("Administrator || PowerUser");

    [Authorize(Roles = "Administrator")]
    public IActionResult ShutDown() =>
        Content("Administrator only");
}
```

- `SetTime` -> pode ser acessado por `Administrator` ou `PowerUser`;
- `ShutDown` -> pode ser acessado por `Administrator` apenas.



### Claims

Claims são declarações ou informações sobre a identidade de um usuário, expressa como pares de chave-valor (ex: Email: fulano@email.com, DataNascimento: 01/01/1990).
Em uma estrutura de autorização baseada em claims, uma ação é liberada com base nas características de um usuário e não apenas em papéis (roles).

As claims carregam os atributos do usuário autenticado e ajudam identificá-lo.

Configurando uma claim

```c#
builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("NOME_POLITICA", policy => {
        policy.RequireClaim("Produtos", "VI");
    });
});
```

**claim vira motivo de “não autorizado” quando ela é usada como requisito de policy**

# Customizando a Program.cs

## Program.cs mais organizada

Idealmente, as configurações dentro da program devem ficar em arquivos isolados. Isso pode ser feito através de Extension Methods

Ex:
```
public static class MvcConfig
    {
        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder) {
            builder.Services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddHsts(opts =>
            {
                opts.Preload = true;
                opts.IncludeSubDomains = true;
                opts.MaxAge = TimeSpan.FromDays(60);
            });
            
            return builder;
        }
    }
```

Na Program.cs basta fazer:
`builder.AddMvcConfiguration();`

# Configurando o ambiente

Em `launchSettings.json` podemos configurar as variáveis e os ambientes que vamos rodar as aplicações. Podemos ter algo como:

```
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "Development": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7014;http://localhost:5296",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Production": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7014;http://localhost:5296",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Production"
      }
    }
  }
}

```

Da até para validar ambiente dentro de views, como:

```
<enviroment include="Development">
</enviroment>

<enviroment exclude="Development">
</enviroment>
```

Neste exemplo, tudo que está dentro de `include` será carregado desde que o ambiente seja `Development` e tudo que está dentro de
`exclude` será desconsiderado se o ambiente for `Development`.

# Lendo configurações

Cada configuração colocada no `appsettings.json` pode ser acessada durante o código.

## Ex 1
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ApiConfiguration": {
    "Domain": "https://minhaapi.com",
    "UserKey": "MinhaChave",
    "UserSecret": "MinhaChave"
  }
}
````
Tendo a estrutura `ApiConfiguration` pode-se criar uma classe que contenham as propriedades `Domain`, `UserKey` e `UserSecret`.
Uma vez que a classe é criada é possíve carregar os valores da seguinte forma:

```json
var apiConfig = new ApiConfiguration();

_configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);
```

`_configuration` é do tipo `IConfiguration`.
## Ex 2
Uma outra forma é fazer a segunte configuração:

`builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection(ApiConfiguration.ConfigName));`

Feito isso é possível acessar as configurações da seguinte forma:

```
public class HomeController : Controller
{
    private readonly ApiConfiguration _apiConfiguration;

    public HomeController(IOptions<ApiConfiguration> apiConfiguration) {
        _apiConfiguration = apiConfiguration;
    }
}
```

Feito a injeção, basta acessar o  `_apiConfiguration`.


# Referências

- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-10.0
- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-10.0
- https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-10.0
- https://pt.stackoverflow.com/questions/16316/asp-net-identity-e-claims
- https://learn.microsoft.com/en-us/archive/blogs/alikl/windows-identity-foundation-wif-by-example-part-iii-how-to-implement-claims-based-authorization-for-asp-net-application