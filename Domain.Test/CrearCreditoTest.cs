using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Domain.Test
{
    public class CrearCreditoTest
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
        public void ValorDeCreditoSolicitadoMenorA5Millones()
        {
            string esperado = "El valor solicitado para el crédito no es permitido";
            List<string> errores = CreditBuilder.CanCreateCredit(50000, 8);
            string obtenido = errores.Contains(esperado) ? esperado : String.Join(',', errores);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ValorDeCreditoSolicitadoMayorA10Millones()
        {
            string esperado = "El valor solicitado para el crédito no es permitido";
            List<string> errores = CreditBuilder.CanCreateCredit(15000000, 8);
            string obtenido = errores.Contains(esperado) ? esperado : String.Join(',', errores);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void PlazoSolicitadoMayorA10Meses()
        {
            string esperado = "El plazo solicitado para el crédito no es permitido";
            List<string> errores = CreditBuilder.CanCreateCredit(5000000, 11);
            string obtenido = errores.Contains(esperado) ? esperado : String.Join(',', errores);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void TasaSolicitadaIncorrecta()
        {
            string esperado = "La tasa no es valida";
            List<string> errores = CreditBuilder.CanCreateCredit(5000000, 11,5);
            string obtenido = errores.Contains(esperado) ? esperado : String.Join(',', errores);
            Assert.AreEqual(esperado, obtenido);
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
    }
}