using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Abono : Entity<int>
    {       
        public double Valor { get; set; }
        public DateTime FechaAbonado { get;  set; }
        public override string ToString()
        {
            return string.Format("ID: {0}\nValor: {1}\nFecha de Abono: {2}", base.Id, Valor, FechaAbonado);
        }
    }
}
