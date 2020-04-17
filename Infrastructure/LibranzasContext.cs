using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class LibranzasContext : DbContextBase
    {
        public LibranzasContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<AbonoCuota> AbonoCuotas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbonoCuota>().HasKey(ac => new {ac.CuotaId , ac.AbonoId});
        }
    }
}
