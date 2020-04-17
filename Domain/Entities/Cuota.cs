using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cuota : Entity<int>, IServicioFinanciero
    {
        public double Valor { get; set; }
        public double Pagado { get; set; }
        public DateTime FechaDePago { get; set; }
        public Estado Estado { get; set; } = Estado.Pendiente;
        public double Saldo { get; set; }
        public virtual List<AbonoCuota> AbonoCuotas { get; set; }
        public Cuota()
        {
            Saldo = Valor;
            AbonoCuotas = new List<AbonoCuota>();
        }

        public string Abonar(double valor)
        {
            Saldo -= valor;
            Pagado += valor;
            return "";
        }
        public void LiquidarCuota(double valor)
        {
            Estado = Estado.Pagado;
            Saldo = 0;
            Pagado = Valor;
        }
        public override string ToString()
        {
            return string.Format("Valor: {0}\nSaldo: {1}\nFecha de Pago: {2}\n", Valor, Saldo, FechaDePago);
        }
    }
    public enum Estado
    {
        Pendiente,
        Pagado
    }
}
