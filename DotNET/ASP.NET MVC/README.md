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