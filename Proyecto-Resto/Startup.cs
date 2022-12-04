using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proyecto_Resto.Context;

namespace Proyecto_Resto
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            // Crear una nueva instancia de nuestro servidor web

            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder); // configuramos con sus respetivos servicios
            var app = builder.Build(); // sobre esta app configuramos luego los middelware
            Configure(app); // configuramos los middleware
            return app;  // retornamos la app ya inicializada
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {

           // builder.Services.AddDbContext<RestoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string fallida")));

            builder.Services.AddDbContext<RestoContext>(options => options.UseInMemoryDatabase("DbContext")); // base con persistencia en memoria de prueba  


            // Add services to the container.
            builder.Services.AddControllersWithViews();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
