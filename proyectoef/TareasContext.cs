/* Contexto: Es donde van a ir todas las relaciones de los modelos que nosotros tenemos para poder 
transformarlo en colecciones que van a representarse dentro de la base de datos.

DBSet: Es un set o una asignación de datos del modelo que nosotros hemos creado previamente, básicamente esto va a representar lo que 
sería una tabla dentro del contexto de entity framework. */

using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;
public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    //
    public TareasContext(DbContextOptions<TareasContext> options) : base(options){}
    

}
