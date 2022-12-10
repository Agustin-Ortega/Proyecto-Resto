using Proyecto_Resto.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Resto.Models
{
    public class Plato
    {
        [Key]
        public int Id { get; set; }

        
        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = MsgError.StringMaxMin)]
        public string nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(100, MinimumLength = 4, ErrorMessage = MsgError.StringMaxMin)]
        public string descricpion { get; set; }

        [Required]
        public int Costo { get; set; }

        [Required]
        public int precio { get; set; } = 0;

        [Required]
        public int stock { get; set; } = 20;
        //public bool stock { get; set; } = true;   alternativa

        [Display(Name ="Foto del plato")]
        public string? Imagen { get; set; }

    }
}