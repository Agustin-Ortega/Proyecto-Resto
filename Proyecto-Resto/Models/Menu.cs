using System.ComponentModel.DataAnnotations;

namespace Proyecto_Resto.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public Opcion tipo { get; set; }
        [Required]
        public int Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }

    }
}
