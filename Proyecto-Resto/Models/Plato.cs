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
        [StringLength(20, MinimumLength = 6, ErrorMessage = MsgError.StringMaxMin)]
        public string nombre { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = MsgError.StringMaxMin)]
        public string descricpion { get; set; }

        [Required]
        public int precio { get; set; } = 0;

        [Required]
        public int stock { get; set; } = 20;
        //public bool stock { get; set; } = true;   alternativa

        [ForeignKey("Menu")]
        public int idMenu { get; set; }
        public Menu Menu { get; set; }
    }
}