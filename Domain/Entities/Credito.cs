using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Credito : Entity<int>, IServicioFinanciero
    {
        public double Valor { get; set; }
        public int Plazo { get; set; }
        public double ValorAPagar { get => Valor * (1 + TasaDeInteres * Plazo); }
        public readonly double TasaDeInteres;
        public double Saldo { get { return ValorAPagar - CalcularPagado(); } private set { Saldo = value; } }
        public DateTime FechaCreacion { get; private set; } = DateTime.Now;
        public List<Cuota> Cuotas { get; set; }
        public List<Pago> Pagos { get; set; }

        public static double VALOR_MINIMO_DE_CREDITO = 5000000;
        public static double VALOR_MAXIMO_DE_CREDITO = 10000000;
        public static double PLAZO_MAXIMO = 10;
        public Credito(double valor, int plazo, double tasaDeInteres)
        {
            Valor = valor;
            Plazo = plazo;
            TasaDeInteres = tasaDeInteres;
            this.Pagos = new List<Pago>();
            GenerarCuotas();
        }
        public string Abonar(double valor)
        {

            if (CanAbonar(valor).Count != 0)
                throw new InvalidOperationException("Operacion Invalida");
            Pagos.Add(new Pago { Valor = valor, FechaPagado = DateTime.Now });
            List<Cuota> cuotasPendientes = GetCuotasPendientes();
            int i = 0;
            do
            {
                Cuota cuota = cuotasPendientes[i];
                if (valor > cuota.Saldo)
                {
                    cuota.Estado = Estado.Pagado;
                    valor -= cuota.Saldo;
                    cuota.Saldo = 0;
                    cuota.Pagado = cuota.Valor;
                }
                else
                {
                    cuota.Saldo -= valor;
                    cuota.Pagado = valor;
                    valor = 0;
                }
                i++;
            } while (valor > 0);
            return $"Abono concluido, saldo pendiente $ {Saldo}";
        }
        public List<string> CanAbonar(double valor)
        {            
            Cuota cuota = GetCuotasPendientes().FirstOrDefault();
            var errores = new List<string>();
            if (valor < cuota.Saldo || valor > Saldo) errores.Add("El valor a abonar es incorrecto");            
            return errores;
        }
        private List<Cuota> GetCuotasPendientes()
        {
            return this.Cuotas.FindAll(cuota => cuota.Estado == Estado.Pendiente).OrderBy(cuota => cuota.FechaDePago).ToList();
        }
        
        private double CalcularPagado()
        {
            double pagado = 0;
            foreach (Cuota cuota in Cuotas)
            {
                pagado += cuota.Pagado;
            }
            return pagado;
        }
        public void GenerarCuotas()
        {
            this.Cuotas = new List<Cuota>();
            for (int i = 1; i <= Plazo; i++)
            {
                this.Cuotas.Add(new Cuota { Valor = ValorAPagar / Plazo, FechaDePago = FechaCreacion.AddMonths(i), Saldo = ValorAPagar/Plazo });
            }
        }

    }


}
