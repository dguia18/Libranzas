using Domain.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Empleado : Entity<int>
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public double Salario { get; set; }
        public List<Credito> Creditos { get; set; }
    }   
}
