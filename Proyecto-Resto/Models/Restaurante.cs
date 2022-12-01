using System.ComponentModel.DataAnnotations;

namespace Proyecto_Resto.Models
{
    public class Restaurante
    {
        [Key]
        public int Id { get;  set; }
        [Required]
        [Display(Name ="Restaurante")]
        public string nombre { get;  set; }
    }
}