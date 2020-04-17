using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class CreditBuilder
    {
        protected CreditBuilder()
        {
        }

        public static List<string> CanCreateCredit(double valor, int plazo, double tasaDeInteres = 0.005)
        {
            var errores = new List<string>();
            if (valor < Credito.VALOR_MINIMO_DE_CREDITO || valor > Credito.VALOR_MAXIMO_DE_CREDITO) errores.Add("El valor solicitado para el crédito no es permitido");
            if(plazo < 0 || plazo >Credito.PLAZO_MAXIMO) errores.Add("El plazo solicitado para el crédito no es permitido");
            if (tasaDeInteres < 0 || tasaDeInteres > 1) errores.Add("La tasa no es valida");
            return errores;
        }
        public static Credito CrearCredito(double valor, int plazo, double tasaDeInteres = 0.005)
        {
            if (CanCreateCredit(valor, plazo, tasaDeInteres).Any())
                throw new InvalidOperationException("Operacion Invalida");
            return new Credito(valor, plazo, tasaDeInteres);
        }
    }
}
