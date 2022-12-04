using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Models;

namespace Proyecto_Resto.Context
{
    public class RestoContext: IdentityDbContext
    {
        public RestoContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        
        public DbSet<Restaurante> Restaurantes { get; set; }

        public DbSet<Plato> Platos { get; set; }
        
        public DbSet<Proyecto_Resto.Models.ItemReserva> ItemReservas { get; set; }

    }
}
