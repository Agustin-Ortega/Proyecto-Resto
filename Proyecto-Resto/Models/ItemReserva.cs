using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Resto.Models
{
    public class ItemReserva
    {
        [Key]
        public int Id { get; set; }

        public bool? ItemProcesado { get; set; }

        public int? idReserva { get; set; }
        [ForeignKey("idReserva")]
        public Reserva? Reserva { get; set; }

        [Display(Name ="Plato")]
        public int idPlato { get; set; }
        [ForeignKey("idPlato")]
        public Plato? Plato { get; set; }


    }
}