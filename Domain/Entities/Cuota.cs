using Domain.Base;
using System;

namespace Domain.Entities
{
    public class Cuota : Entity<int>, IServicioFinanciero
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
            return string.Format("ID: {0}\nValor: {1}\nSaldo: {2}\nFecha de Pago: {3}\n", base.Id, Valor, Saldo, FechaDePago);
        }
    }
    public enum Estado
    {
        Pendiente,
        Pagado
    }
}
