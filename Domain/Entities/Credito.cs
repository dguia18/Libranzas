using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Credito : Entity<int>, ICredito
    {
        public double Valor { get; set; }
        public int Plazo { get; set; }
        public double ValorAPagar { get => Valor * (1 + TasaDeInteres * Plazo); }
        public readonly double TasaDeInteres;
        public double Saldo { get { return ValorAPagar - CalcularPagado(); } }
        public DateTime FechaCreacion { get; private set; } = DateTime.Now;
        public List<Cuota> Cuotas { get; set; }
        public List<Pago> Pagos { get; set; }

        public static double VALOR_MINIMO_DE_CREDITO = 5000000;
        public static double VALOR_MAXIMO_DE_CREDITO = 10000000;
        public static double PLAZO_MAXIMO = 10;

        public void Abonar(double valor)
        {
            throw new NotImplementedException();
        }
        public Credito(double valor, int plazo, double tasaDeInteres)
        {
            Valor = valor;
            Plazo = plazo;
            TasaDeInteres = tasaDeInteres;                   
            this.Pagos = new List<Pago>();
            GenerarCuotas();
        }
        private double CalcularPagado()
        {
            double pagado=0;
            foreach (Cuota cuota in Cuotas)
            {
                pagado += cuota.Estado == Estado.Pendiente ? cuota.Valor : 0;
            }
            return pagado;
        }
        public void GenerarCuotas()
        {
            this.Cuotas = new List<Cuota>();
            for (int i = 1; i <= Plazo; i++)
            {
                this.Cuotas.Add(new Cuota {Valor = Valor / Plazo, FechaDePago = FechaCreacion.AddMonths(i) });
            }
        }

    }

    
}
