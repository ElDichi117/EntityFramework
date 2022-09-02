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
        modelBuilder.Entity<Categoria>(categoria=>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p=> p.CategoriaId);
            categoria.Property(p=> p.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(p=>p.Descripcion);
        });

        modelBuilder.Entity<Tarea>(tarea=>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t=> t.TareaId);

            //Se hace la configuración de la llave foranea
                //Tiene una  Propiedad    Con muchos Tareas (colección que viene en el modelo)            Lllave foranea     
            tarea.HasOne(p=> p.Categoria).WithMany(p=> p.Tareas).HasForeignKey(p=> p.CategoriaId);

            tarea.Property(p=> p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p=> p.Descripcion);

            tarea.Property(p=> p.PrioridadTarea);

            tarea.Property(p=> p.FechaCreacion);

            tarea.Ignore(p=> p.Resumen);
        
        });
    }
}
