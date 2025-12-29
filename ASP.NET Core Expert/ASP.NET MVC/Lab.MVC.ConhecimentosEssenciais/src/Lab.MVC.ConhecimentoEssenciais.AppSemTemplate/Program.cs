using Lab.MVC.AppSemTemplate.Data;
using Lab.MVC.AppSemTemplate.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

builder.Services.AddControllersWithViews(); // Configuração mais simples que o AddMVC

//builder.Services.AddRouting(options =>
//{
//    options.ConstraintMap["slugify"] = typeof(RouteSlugifyParameterTransformer);
//});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Permite olhar a pasta wwwroot
app.UseStaticFiles();

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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
