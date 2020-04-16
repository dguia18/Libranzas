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
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Credito> Credito { get; set; }
        public DbSet<Abono> Abono { get; set; }
        public DbSet<Cuota> Cuota { get; set; }


    }
}
