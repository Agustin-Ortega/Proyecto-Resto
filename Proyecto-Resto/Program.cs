
//var builder = WebApplication.CreateBuilder(args);

namespace Proyecto_Resto
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var app = Startup.InicializarApp(args);  // pasamos los argumentos que son recibidos en la ejecucion
            app.Run();

        }
    }
}

