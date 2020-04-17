using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;

namespace Application.Test
{
    class CrearCreditoServiceTest
    {
        LibranzasContext _context;
        UnitOfWork unitOfWork;
        EmpleadoService crearEmpleadoService;
        CreditoService crearCreditoService;
        [SetUp]
        public void SetUp()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<LibranzasContext>().UseInMemoryDatabase("Libranzas").Options;

            _context = new LibranzasContext(optionsInMemory);            
            unitOfWork = new UnitOfWork(_context);
            crearEmpleadoService = new EmpleadoService(unitOfWork);
            var request = new CrearEmpleadoRequest { Cedula = "1065840833", Nombre = "Duvan", Salario = 1200000 };
            crearEmpleadoService.CrearEmpleado(request);
            crearCreditoService = new CreditoService(unitOfWork);
        }
        [TestCaseSource("TestDataIncorrectosValoresDelCredito")]
        public void CrearCreditoConValoresIncorrectos(string cedulaEmpleado, string numero, double valor,
                                        int plazo, double tasaDeInteres,string esperado)
        {
            var request = new CrearCreditoRequest { CedulaEmpleado = cedulaEmpleado, Numero = numero, Plazo = plazo, TasaDeInteres = tasaDeInteres, Valor = valor };            
            var response = crearCreditoService.CrearCredito(request);
            string obtenido = response.Mensaje.Contains(esperado) ? esperado : String.Join(',', response.Mensaje);
            Assert.AreEqual(esperado, obtenido);
        }
        private static IEnumerable TestDataIncorrectosValoresDelCredito()
        {            
            yield return new TestCaseData("1065840833", "00001", 50000, 8, 0.005, "El valor solicitado para el crédito no es permitido").SetName("ValorDeCreditoSolicitadoMenorA5Millones");
            yield return new TestCaseData("1065840833", "00001", 15000000, 8, 0.005, "El valor solicitado para el crédito no es permitido").SetName("ValorDeCreditoSolicitadoMayorA10Millones");
            yield return new TestCaseData("1065840833", "00001", 5000000, 11, 0.005, "El plazo solicitado para el crédito no es permitido").SetName("PlazoSolicitadoMayorA10Meses");
            yield return new TestCaseData("1065840833", "00001", 5000000, 8, 5, "La tasa no es valida").SetName("TasaSolicitadaIncorrecta");
            yield return new TestCaseData("98032461204", "00001", 50000, 8, 0.005, $"El número de cedula 98032461204 no existe").SetName("CedulaDeEmpleadoNoEncontrado");
            yield return new TestCaseData("1065840833", "00001", 5000000, 9, 0.005, "El valor a total para el crédito es $5225000").SetName("CreditoCorrecto");
        }
       
        [TestCaseSource("TestDataRepetido")]
        public void CreditoRepetido(string cedulaEmpleado, string numero, double valor,
                                        int plazo, double tasaDeInteres, string esperado)
        {
            var request = new CrearCreditoRequest { CedulaEmpleado = cedulaEmpleado, Numero = numero, Plazo = plazo, TasaDeInteres = tasaDeInteres, Valor = valor };
            crearCreditoService.CrearCredito(request);
            var response = crearCreditoService.CrearCredito(request);
            string obtenido = response.Mensaje.Contains(esperado) ? esperado : String.Join(',', response.Mensaje);
            Assert.AreEqual(esperado, obtenido);
        }
        private static IEnumerable TestDataRepetido()
        {
            yield return new TestCaseData("1065840833", "0001", 5000000, 8, 0.005, $"El número de credito 0001 ya existe").SetName("NumeroDeCreditoRepetido");
        }
    }
}
