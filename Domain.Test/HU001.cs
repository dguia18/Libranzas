using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Domain.Test
{
    public class HU001
    {
        Empleado empleado;
        [SetUp]
        public void Setup()
        {
            empleado = new Empleado();
            empleado.Cedula = "1065840833";
            empleado.Nombre = "Duvan";
            empleado.Salario = 1200000;
        }
        
        [Test]
        public void EjecutarMetodoDeCrearSinAntesValidarCAN()
        {
            string esperado = "Operacion Invalida";
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => CreditBuilder.CrearCredito(5000000, 11, 5));            
            Assert.AreEqual(esperado, ex.Message);
        }
        [Test]
        public void CréditoSolicitadoCorrecto()
        {
            CreditBuilder.CanCreateCredit(5000000, 9);
            string esperado = "El valor a total para el crédito es $5225000";
            string obtenido = "El valor a total para el crédito es $";
            var credito = CreditBuilder.CrearCredito(5000000, 9);
            if (credito != null)
            {
                empleado.Creditos.Add(credito);
                obtenido += credito.ValorAPagar;
            }            
            Assert.AreEqual(esperado, obtenido);
        }
        [TestCaseSource("TestData")]
        public void CrearCreditoTest(double valor, int plazo, double tasa, string esperado)
        {
            List<string> errores = CreditBuilder.CanCreateCredit(valor, plazo, tasa);
            string obtenido = errores.Contains(esperado) ? esperado : String.Join(',', errores);
            Assert.AreEqual(esperado, obtenido);
        }
        private static IEnumerable TestData()
        {
            yield return new TestCaseData(50000, 8, 0.005, "El valor solicitado para el crédito no es permitido").SetName("ValorDeCreditoSolicitadoMenorA5Millones");
            yield return new TestCaseData(15000000, 8, 0.005, "El valor solicitado para el crédito no es permitido").SetName("ValorDeCreditoSolicitadoMayorA10Millones");
            yield return new TestCaseData(5000000, 11, 0.005, "El plazo solicitado para el crédito no es permitido").SetName("PlazoSolicitadoMayorA10Meses");
            yield return new TestCaseData(5000000, 8, 5, "La tasa no es valida").SetName("TasaSolicitadaIncorrecta");
        }
    }
}