using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"))
//Lo que hace aquí es buscar la cadena de conexión
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    //Aquí lo que se hace es crear y verificar la base de datos
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
