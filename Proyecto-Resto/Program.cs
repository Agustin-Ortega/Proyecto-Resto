
////var builder = WebApplication.CreateBuilder(args);

//namespace Proyecto_Resto
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//            var app = Startup.InicializarApp(args);  // pasamos los argumentos que son recibidos en la ejecucion
//            app.Run();

//        }
//    }
//}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestoContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string fallida")));

 // builder.Services.AddDbContext<RestoContext>(options => options.UseInMemoryDatabase("DbContext")); // base con persistencia en memoria de prueba  

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<RestoContext>();

builder.Services.AddControllersWithViews();

 builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;


app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();