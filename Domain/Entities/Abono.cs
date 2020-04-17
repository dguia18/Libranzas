using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Abono : Entity<int>
    {       
        public double Valor { get; set; }
        public DateTime FechaAbonado { get;  set; }
        public virtual List<AbonoCuota> AbonoCuotas{ get; set; }
        public Abono()
        {
            this.AbonoCuotas = new List<AbonoCuota>();
        }
        public override string ToString()
        {
            return string.Format("Valor: {0}\nFecha de Abono: {1}", Valor, FechaAbonado);
        }
    }
}
