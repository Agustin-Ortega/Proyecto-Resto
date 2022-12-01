using Microsoft.EntityFrameworkCore.Internal;
using Proyecto_Resto.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Resto.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get;  set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        [Display(Name = "Fecha de reserva")]
        public DateTime Fecha { get; set; }

        
        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = "Cantidad")]
        [Range(1, 10, ErrorMessage = MsgError.StringMaxMin)]
        public int CantidadPersonas { get;  set; }


        [ForeignKey("Cliente")]
        //los hacemos nulleables
        public int idCliente { get; set; }
        public Cliente Cliente { get; set; }


        [ForeignKey("Menu")]
        public int idMenu { get;  set; }
        public Menu Menu { get;  set; }

        [ForeignKey("Restaurante")]
        public int idRestaurante { get; set; }
        public Restaurante Restaurante { get; set; }

    }
}
