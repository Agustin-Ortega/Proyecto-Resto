using System.ComponentModel.DataAnnotations;

namespace Proyecto_Resto.Models
{
    public class Menu
    {
       
        public int Id { get; set; }
        public Opcion tipo { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }

    }
}
