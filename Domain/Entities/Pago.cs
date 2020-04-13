using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pago : Entity<int>
    {       
        public double Valor { get; set; }
        public DateTime FechaPagado { get;  set; }
       
    }
}
