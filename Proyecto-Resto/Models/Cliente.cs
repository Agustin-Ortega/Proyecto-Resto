using Proyecto_Resto.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Proyecto_Resto.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10,MinimumLength =3, ErrorMessage = MsgError.StringMaxMin)]
        // otra forma
        //[MaxLength(10, ErrorMessage ="No puede tener mas de 10 caracteres")]
        //[MinLength(4, ErrorMessage = "No puede tener menos de 4 caracteres")]
        public string Nombre { get;  set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10, MinimumLength = 3, ErrorMessage = MsgError.StringMaxMin)]
        public string Apellido { get;   set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = MsgError.StringMaxMin)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        public string Email { get;  set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = MsgError.StringMaxMin)]
        [DataType(DataType.Password)]
        public string password { get;  set; }

        public bool isAdmin { get; set; } = false;

        public List<Reserva>? reservas { get;  set; }


    }
}
