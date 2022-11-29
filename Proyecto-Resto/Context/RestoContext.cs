 using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Models;

namespace Proyecto_Resto.Context
{
    public class RestoContext: DbContext
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

    }
}
