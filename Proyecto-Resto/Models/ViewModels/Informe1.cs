using System.ComponentModel.DataAnnotations;

namespace Proyecto_Resto.Models.ViewModels
{
    public class Informe1
    {

        public DateTime Fecha { get; set; }

        [Display(Name ="Nombre y apellido")]
        public string Cliente { get; set; }

        public List<string> ListaPlatos { get; set; }

        [Display(Name ="Precio de Venta")]
        public double Total { get; set; }
         
        public double Ganancia { get; set; }
    }
}
