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
            empleado.Creditos.Add(CreditBuilder.CrearCredito(valor:7000000,plazo: 4).InicializarNumero("0001"));
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
        public void ValorAAbonarIgualQueLaCuota()
        {
            empleado.Creditos.Find(x => x.Numero == "0001").Abonar(1785000);
            var cuotas = empleado.Creditos.Find(x => x.Numero == "0001").Cuotas;
            var cuotasEsperadas = CuotasGeneradasPorAbonoIgualACuota();
            Assert.AreEqual(cuotasEsperadas, cuotas);
        }
        private List<Cuota> CuotasGeneradasPorAbonoIgualACuota()
        {
            empleado = new Empleado();
            empleado.Cedula = "1065840833";
            empleado.Nombre = "Duvan";
            empleado.Salario = 1200000;
            empleado.Creditos.Add(CreditBuilder.CrearCredito(7000000, 4).InicializarNumero("0001"));
            var credito = empleado.Creditos.Find(x => x.Numero == "0001");
            for (int i = 0; i < 4; i++)
            {
                credito.Cuotas[i].Estado = Estado.Pendiente;
                credito.Cuotas[i].Saldo = 1785000;
                credito.Cuotas[i].Valor = 1785000;
                credito.Cuotas[i].Pagado = 0;
            }
            credito.Cuotas[0].Estado = Estado.Pagado;
            credito.Cuotas[0].Saldo = 0;
            credito.Cuotas[0].Pagado = 1785000; 
            return credito.Cuotas;
        }
        [Test]
        public void ValorAAbonarMayorAlValorDeCuota()
        {
            var credito = empleado.Creditos.Find(x => x.Numero == "0001");
            credito.Abonar(1790000);
            var cuotasEsperadas = CuotasGeneradasPorAbonoMayorACuota();
            Assert.AreEqual(credito.Cuotas,cuotasEsperadas);
        }
        private List<Cuota> CuotasGeneradasPorAbonoMayorACuota()
        {
            empleado = new Empleado();
            empleado.Cedula = "1065840833";
            empleado.Nombre = "Duvan";
            empleado.Salario = 1200000;
            empleado.Creditos.Add(CreditBuilder.CrearCredito(7000000, 4).InicializarNumero("0001"));
            var credito = empleado.Creditos.Find(x => x.Numero == "0001");
            for (int i = 0; i < 4; i++)
            {
                credito.Cuotas[i].Estado = Estado.Pendiente;
                credito.Cuotas[i].Saldo = 1785000;
                credito.Cuotas[i].Valor = 1785000;
                credito.Cuotas[i].Pagado = 0;
            }
            credito.Cuotas[0].Estado = Estado.Pagado;
            credito.Cuotas[0].Saldo = 0;
            credito.Cuotas[0].Pagado = 1785000;
            credito.Cuotas[1].Saldo = 1780000;
            credito.Cuotas[1].Pagado = 5000;
            return credito.Cuotas;
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
