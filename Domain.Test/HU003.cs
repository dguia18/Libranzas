using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Domain.Test
{
    class HU003
    {
        Empleado empleado;
        [SetUp]
        public void SetUp()
        {
            empleado = new Empleado();
            empleado.Cedula = "1065840833";
            empleado.Nombre = "Duvan";
            empleado.Salario = 1200000;
            empleado.Creditos.Add(CreditBuilder.CrearCredito(7000000, 4));
        }
        [Test]
        public void CuotasDelCredito()
        {
            empleado.Creditos[0].Abonar(5000000);
            List<string> esperado = empleado.Creditos[0].GetCuotasString();
            List<string> obtenido = esperado;
            Assert.AreEqual(esperado, obtenido);
        }
    }
}
