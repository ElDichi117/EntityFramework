using System.ComponentModel.DataAnnotations;

namespace proyectoef.Models;

/* La palabra clave virtual se utiliza para modificar un método, propiedad, 
indizador o declaración de evento y permite invalidar cualquiera de estos 
elementos en una clase derivada. */
public class Categoria 
{
    //Key es para la clave de esta tabla 
    [Key]
    public Guid CategoriaId {get;set;}
    
    [Required]
    [MaxLength(150)]
    public string Nombre { get; set; }

    public string Descripcion { get; set; }
    public virtual ICollection<Tarea> Tareas{ get; set; }
}