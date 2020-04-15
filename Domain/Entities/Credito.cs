using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Credito : Entity<int>, IServicioFinanciero
    {
        public string Numero { get; set; }
        public double Valor { get; set; }
        public int Plazo { get; set; }
        public double ValorAPagar { get => Valor * (1 + TasaDeInteres * Plazo); }
        public readonly double TasaDeInteres;
        public double Saldo { get { return ValorAPagar - CalcularPagado(); } private set { Saldo = value; } }
        public DateTime FechaCreacion { get; private set; } = DateTime.Now;
        public List<Cuota> Cuotas { get; set; }
        public List<Abono> Abonos { get; set; }

        public static double VALOR_MINIMO_DE_CREDITO = 5000000;
        public static double VALOR_MAXIMO_DE_CREDITO = 10000000;
        public static double PLAZO_MAXIMO = 10;
        public Credito(double valor, int plazo, double tasaDeInteres)
        {
            Valor = valor;
            Plazo = plazo;
            TasaDeInteres = tasaDeInteres;
            this.Abonos = new List<Abono>();
            GenerarCuotas();
        }
        public Credito()
        {

        }
        public string Abonar(double valor)
        {

            if (CanAbonar(valor).Count != 0)
                throw new InvalidOperationException("Operacion Invalida");
            Abonos.Add(new Abono { Valor = valor, FechaAbonado = DateTime.Now });
            List<Cuota> cuotasPendientes = GetCuotasPendientes();
            int i = 0;
            do
            {
                Cuota cuota = cuotasPendientes[i];
                if (valor > cuota.Saldo)
                {
                    cuota.LiquidarCuota(valor);
                    valor -= cuota.Valor;
                }
                else
                {
                    cuota.Abonar(valor);
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
            double pagado=0;
            this.Cuotas.ForEach(cuota => pagado += cuota.Pagado);            
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
        public List<string> GetAbonosString()
        {
            List<string> abonos = new List<string>();
            this.Abonos.ForEach(
                abono => abonos.Add(abono.ToString())
                );
            return abonos;
        }
        public List<string> GetCuotasString()
        {
            List<string> cuotas = new List<string>();
            this.Cuotas.ForEach(
                cuota => cuotas.Add(cuota.ToString())
                );
            return cuotas;
        }
    }


}
