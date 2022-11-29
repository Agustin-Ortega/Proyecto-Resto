using Proyecto_Resto.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Proyecto_Resto.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10,MinimumLength =3, ErrorMessage = MsgError.StringMaxMin)]  
        public string Nombre { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [StringLength(10, MinimumLength = 3, ErrorMessage = MsgError.StringMaxMin)]
        public string Apellido { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Numero Telefono")]
        public int Telefono { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
        

    }
}
