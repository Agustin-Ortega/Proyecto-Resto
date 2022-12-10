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

        [Display(Name ="Foto del Restaurante")]
        public string? Imagen { get; set; }

        public string? Direccion { get; set; }

        public string? Url { get; set; }
    }
}