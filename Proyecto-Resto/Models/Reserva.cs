using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Resto.Models
{
    public class Reserva
    {
        public int Id { get; set; } 

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime Fecha { get; set; }

        public List<Menu>menus { get; set; }

        [Required]
        public int Telefono { get; set; }
        [Required]
        public string Nombre { get; set; }
        
    }
}
