using Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Test
{
    class HU002
    {
        Empleado empleado;
        [SetUp]
        public void SetUp()
        {
            empleado = new Empleado();
            empleado.Cedula = "1065840833";
            empleado.Nombre = "Duvan";
            empleado.Salario = 1200000;
            empleado.Creditos.Add(CreditBuilder.CrearCredito(7000000, 4).InicializarNumero("0001"));
        }
        [Test]
        public void ValorAAbonarNegativo()
        {
            string esperado = "El valor a abonar es incorrecto";
            List<string> respuesta = empleado.Creditos.Find(x=>x.Numero =="0001").CanAbonar(-500);
            string obtenido =respuesta.Contains(esperado) ? esperado : String.Join(',', respuesta);
            Assert.AreEqual(esperado, obtenido);
        }        
        [Test]
        public void ValorAAbonarMenorAlMinimoValorDeLaCuota()
        {
            string esperado = "El valor a abonar es incorrecto";
            List<string> respuesta = empleado.Creditos.Find(x => x.Numero == "0001").CanAbonar(1500000);
            string obtenido = respuesta.Contains(esperado) ? esperado : String.Join(',', respuesta);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ValorAAbonarMayorAlSaldoRestante()
        {
            string esperado = "El valor a abonar es incorrecto";
            List<string> respuesta = empleado.Creditos.Find(x => x.Numero == "0001").CanAbonar(8000000);
            string obtenido = respuesta.Contains(esperado) ? esperado : String.Join(',', respuesta);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void ValorAAbonarCorrecto()
        {
            string esperado = $"Abono concluido, saldo pendiente $ {2140000}";
            string obtenido = empleado.Creditos.Find(x => x.Numero == "0001").Abonar(5000000);
            Assert.AreEqual(esperado, obtenido);
        }
        [Test]
        public void EjecutarMetodoDeAbonarSinAntesValidarCAN()
        {
            string esperado = "Operacion Invalida";
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => empleado.Creditos.Find(x => x.Numero == "0001").Abonar(50000000));
            Assert.AreEqual(esperado, ex.Message);
        }       

    }
}
