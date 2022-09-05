using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using proyectoef;
using proyectoef.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"))
//Lo que hace aquí es buscar la cadena de conexión
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


//Se hace el mapeo de la ruta de acceso al método
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    //Aquí lo que se hace es crear y verificar la base de datos
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    //Aquí como no se esta haciendo una filtración nos estamos trayendo la colección de datos que exista en la BD
        //Dentro de la tabla tareas
    //return Results.Ok(dbContext.Tareas);

    //En la siguiente línea se hace uso de LinQ y expresiones lamda para el filtrado de la información

    //Include nos va a ayudar a traer la información dentro de la tarea
   // return Results.Ok(dbContext.Tareas.Include(p=> p.Categoria).Where(p=> p.PrioridadTarea == proyectoef.Models.Prioridad.Baja));

    return Results.Ok(dbContext.Tareas.Include(p=> p.Categoria));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea)=>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;

    //Debes usar async/await cuando tengas una tarea que tome tiempo considerable y debas esperar a que termine
    await dbContext.AddAsync(tarea);
    //await dbContext.Tareas.AddAsync(tarea);

    await dbContext.SaveChangesAsync();

    return Results.Ok();   
});

app.Run();
