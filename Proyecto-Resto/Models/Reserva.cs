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
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        [Display(Name = "Fecha de reserva")]
        public DateTime Fecha { get; set; }


        [Required(ErrorMessage = MsgError.Requerido)]
        [Display(Name = "Cantidad")]
        [Range(1, 5, ErrorMessage = MsgError.StringMaxMin)]
        public int CantidadPersonas { get; set; }

        //los hacemos nulleables
        [Display(Name ="Cliente")]
        public int? idCliente { get; set; }
        [ForeignKey("idCliente")]
        public Cliente? Cliente { get; set; }

        public List<ItemReserva>? ItemReserva { get; set; }
        public double Total { get; set; }

        [Display(Name = "Restaurante")]
        public int? idRestaurante { get; set; }
        [ForeignKey("idRestaurante")]
        public Restaurante? Restaurante { get; set; }

        public bool? isProcessed { get; set; }

    }
}
