using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;

namespace Application.Test
{
    public class CrearEmpleadoTest
    {
        LibranzasContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<LibranzasContext>().UseInMemoryDatabase("Libranzas").Options;

            _context = new LibranzasContext(optionsInMemory);
        }

        [Test]
        public void RegistrarEmpleadoRepetidoTest()
        {
            var request = new CrearEmpleadoRequest { Cedula = "1065840833", Nombre= "Duvan", Salario= 1200000};
            CrearEmpleadoService _service = new CrearEmpleadoService(new UnitOfWork(_context));
            _service.Ejecutar(request);
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"El empleado con numero de cedula 1065840833 ya se encuentra registrado", response.Mensaje);
        }
        [Test]
        public void RegistrarEmpleadoTest()
        {
            var request = new CrearEmpleadoRequest { Cedula = "98032461204", Nombre= "Duvan", Salario= 1200000};
            CrearEmpleadoService _service = new CrearEmpleadoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual($"Se registro con exito el empleado Duvan.", response.Mensaje);
        }
        
    }
}