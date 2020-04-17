using Domain.Entities;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        [Explicit]
        public void ConsultarEmpledoConCreditosEfTest()
        {

            var optionsSqlServer = new DbContextOptionsBuilder<LibranzasContext>()
             .UseSqlServer(@"Server=LAPTOP-GEQ2K9D2\MSSQLSERVER01;Database=Libranzas;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;
            List<Empleado> empleados=null;
            _context = new LibranzasContext(optionsSqlServer);
            try
            {
                empleados = _context.Empleados.Include(i => i.Creditos).Where(t => t.Cedula == "1065840833").ToList();
                //empleados = _context.Empleado.Include(i => i.Creditos).Where(t => t.Cedula == "1065840833").ToList();
            }
            catch (System.Exception)
            {
                throw;
            }

            Assert.IsNotNull(empleados);
        }

        [Test]
        public void RegistrarEmpleadoRepetidoTest()
        {
            var request = new CrearEmpleadoRequest { Cedula = "1065840833", Nombre= "Duvan", Salario= 1200000};
            EmpleadoService _service = new EmpleadoService(new UnitOfWork(_context));
            _service.CrearEmpleado(request);
            var response = _service.CrearEmpleado(request);
            Assert.AreEqual($"El empleado con numero de cedula 1065840833 ya se encuentra registrado", response.Mensaje);
        }
        [Test]
        public void RegistrarEmpleadoTest()
        {
            var request = new CrearEmpleadoRequest { Cedula = "98032461204", Nombre= "Duvan", Salario= 1200000};
            EmpleadoService _service = new EmpleadoService(new UnitOfWork(_context));
            var response = _service.CrearEmpleado(request);
            Assert.AreEqual($"Se registro con exito el empleado Duvan.", response.Mensaje);
        }
        
    }
}