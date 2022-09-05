/* Contexto: Es donde van a ir todas las relaciones de los modelos que nosotros tenemos para poder 
transformarlo en colecciones que van a representarse dentro de la base de datos.

DBSet: Es un set o una asignación de datos del modelo que nosotros hemos creado previamente, básicamente esto va a representar lo que 
sería una tabla dentro del contexto de entity framework. */

using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;
public class TareasContext: DbContext
{

    //Estas son las tablas que se crearon en la DB
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    //
    public TareasContext(DbContextOptions<TareasContext> options) : base(options){}
    

    //Se van a diseñar las restrcciones para el modelo de categoía usando Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //  creando colección para hacer un insersión de datos
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Nombre = "Actividades personales", Peso = 50});


        modelBuilder.Entity<Categoria>(categoria=>
        {
            categoria.ToTable("Categoria");

            categoria.HasKey(p=> p.CategoriaId);

            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p=>p.Descripcion).IsRequired(false);

            categoria.Property(p=> p.Peso);

            categoria.HasData(categoriasInit); 
        });

        List<Tarea> tareasInit = new List<Tarea>();

        //Cuando le asignamos el id de categoria en la tarea, debe ser el mismo qeu la categoria

        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), PrioridadTarea = Prioridad.Media, Titulo = "Pago de servicios publicos", FechaCreacion = DateTime.Now });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), PrioridadTarea = Prioridad.Baja, Titulo = "Terminar de ver pelicula en netflix", FechaCreacion = DateTime.Now });


        modelBuilder.Entity<Tarea>(tarea=>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t=> t.TareaId);

            //Se hace la configuración de la llave foranea
                //Tiene una  Propiedad    Con muchos Tareas (colección que viene en el modelo)            Lllave foranea     
            tarea.HasOne(p=> p.Categoria).WithMany(p=> p.Tareas).HasForeignKey(p=> p.CategoriaId);

            tarea.Property(p=> p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=> p.Descripcion).IsRequired(false);

            tarea.Property(p=> p.PrioridadTarea);

            tarea.Property(p=> p.FechaCreacion);

            tarea.Ignore(p=> p.Resumen);

            tarea.Property(p=> p.Profesor).IsRequired(false);

            //Dice la colección de datos iniciales
            tarea.HasData(tareasInit);
        });
    }
}
