using Application;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<LibranzasContext>()
             .UseInMemoryDatabase("Banco")
             .Options;

            LibranzasContext context = new LibranzasContext(optionsInMemory);
            
            CrearCuentaBancaria(context);
            ConsignarCuentaBancaria(context);
        }

        private static void ConsignarCuentaBancaria(LibranzasContext context)
        {
            #region  Consignar

            AbonarService _service = new AbonarService(new UnitOfWork(context));
            var request = new AbonarRequest() { NumeroCuenta = "524255", Valor = 1000 };

            AbonarResponse response = _service.Ejecutar(request);

            System.Console.WriteLine(response.Mensaje);
            #endregion
            System.Console.ReadKey();
        }

        private static void CrearCuentaBancaria(LibranzasContext context)
        {
            #region  Crear

            CrearCreditoService _service = new CrearCreditoService(new UnitOfWork(context));
            var requestCrer = new CrearCreditoRequest() { TasaDeInteres = "524255", CedulaEmpleado = "Boris Arturo" };

            CrearCuentaBancariaResponse responseCrear = _service.Ejecutar(requestCrer);

            System.Console.WriteLine(responseCrear.Mensaje);
            #endregion
        }
    }
}
