using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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


    }
}
