using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;

namespace Application.Test
{
    class AbonarServiceTest
    {
        LibranzasContext _context;        
        UnitOfWork unitOfWork;
        private EmpleadoService empleadoService;
        private CreditoService creditoService;

        [SetUp]
        public void SetUp()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
            .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;*/
            var optionsInMemory = new DbContextOptionsBuilder<LibranzasContext>().UseInMemoryDatabase("Libranzas").Options;

            _context = new LibranzasContext(optionsInMemory);
            unitOfWork = new UnitOfWork(_context);
            CrearEmpleado();
            CrearCredito();            
        }

        private void CrearCredito()
        {
            creditoService = new CreditoService(unitOfWork);
            var requestCredito = new CrearCreditoRequest { CedulaEmpleado = "1065840833", Numero = "0001", Plazo = 4, TasaDeInteres = 0.005, Valor = 7000000 };
            creditoService.CrearCredito(requestCredito);
        }

        private void CrearEmpleado()
        {
            empleadoService = new EmpleadoService(unitOfWork);
            var requestEmpleado = new CrearEmpleadoRequest { Cedula = "1065840833", Nombre = "Duvan", Salario = 1200000 };
            empleadoService.CrearEmpleado(requestEmpleado);
        }

        [TestCaseSource("TestData")]
        public void AbonarTest(string cedulaEmpleado, string numero, double valor, string esperado)
        {
            var request = new AbonarRequest { CedulaEmpleado = cedulaEmpleado, NumeroCredito = numero, Valor = valor };
            var response = creditoService.Abonar(request);
            string obtenido = response.Mensaje.Contains(esperado) ? esperado : String.Join(',', response.Mensaje);
            Assert.AreEqual(esperado, obtenido);
        }
        private static IEnumerable TestData()
        {            
            yield return new TestCaseData("1065840833", "0001", 8000000, "El valor a abonar es incorrecto").SetName("ValorAAbonarMayorAlSaldoRestante");
            yield return new TestCaseData("1065840833", "0001", 1500000, "El valor a abonar es incorrecto").SetName("ValorAAbonarMenorAlMinimoValorDeLaCuota");
            yield return new TestCaseData("1065840833", "0001", -500, "El valor a abonar es incorrecto").SetName("ValorAAbonarNegativo");
            yield return new TestCaseData("106584083", "0001", -500, $"El empleado con cedula 106584083 no se encuentra registrado en el sistema").SetName("EmpleadoNoEncontrado");
            yield return new TestCaseData("1065840833", "0002", 5000000, $"Señor Duvan, hasta el momento no tiene un credito de numero 0002").SetName("CreditoNoEncontrado"); 
            yield return new TestCaseData("1065840833", "0001", 5000000, $"Abono concluido, saldo pendiente $ {2140000}").SetName("ValorAAbonarCorrecto");
        }        
    }
}
