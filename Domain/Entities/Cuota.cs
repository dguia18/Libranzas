using Domain.Base;
using System;

namespace Domain.Entities
{
    public class Cuota : Entity<int>
    {
        public double Valor { get; set; }
        public double Pagado { get; set; }
        public DateTime FechaDePago { get; set; }
        public Estado Estado { get; set; } = Estado.Pendiente;
        public double Saldo { get; set; } 
        public Cuota()
        {
            Saldo = Valor;
        }
    }
    public enum Estado
    {
        Pendiente,
        Pagado
    }
}
