using Lab.CustomProgram.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddMvcConfiguration();

var app = builder.Build();

app.UseMvcConfiguration();

app.Run();
