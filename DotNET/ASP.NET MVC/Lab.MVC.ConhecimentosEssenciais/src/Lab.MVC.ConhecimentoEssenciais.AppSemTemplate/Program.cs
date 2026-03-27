using Lab.MVC.AppSemTemplate.Configuration;
using Lab.MVC.AppSemTemplate.Data;
using Lab.MVC.AppSemTemplate.Extensions;
using Lab.MVC.AppSemTemplate.Services;
using Lab.MVC.AppSemTemplate.Services.Contracts;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.Configure<RazorViewEngineOptions>(opt =>
//{
//    opt.AreaViewLocationFormats.Clear();
//    opt.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
//    opt.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
//    opt.AreaViewLocationFormats.Add("/Modulos/Shared/{0}.cshtml");
//});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Services.AddResponseCaching();

builder.Services.AddControllersWithViews(); // Configuração mais simples que o AddMVC

// Salva em um diretório as chaves de proteção de dados, para serem compartilhadas entre múltiplas instâncias da aplicação.
builder.Services.AddDataProtection()
.PersistKeysToFileSystem(new DirectoryInfo(@"/var/data_protection_keys/"))
.SetApplicationName("MinhaAPPMVC");

builder.Services.Configure<CookiePolicyOptions>(opts =>
{
    opts.CheckConsentNeeded = context => true;
    opts.MinimumSameSitePolicy = SameSiteMode.None;
    opts.ConsentCookieValue = "true"; // Grava true quando o usuário consentir.
});

//builder.Services.AddRouting(options =>
//{
//    options.ConstraintMap["slugify"] = typeof(RouteSlugifyParameterTransformer);
//});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Inicia a execução no momento que a aplicação é iniciada.
builder.Services.AddHostedService<HostedExampleService>();

builder.Services.AddScoped<IImageUploadService, ImageUploadService>();

var app = builder.Build();

app.UseResponseCaching();

// Permite olhar a pasta wwwroot
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

//app.MapControllerRoute(
//    name: "blog",
//    pattern: "blog/{controller=Home}/{action=Index}/{id?}"
//);

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}"
//);

app.MapControllerRoute(
    name: "default",
    pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

DbMigrationHelper.EnsureSeedData(app).Wait();

app.Run();
