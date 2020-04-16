using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class CrearCreditoService
    {
        readonly IUnitOfWork _unitOfWork;

        public CrearCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearCreditoResponse Ejecutar(CrearCreditoRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(t => t.Cedula == request.CedulaEmpleado);
            if (empleado == null)
            {
                return new CrearCreditoResponse() { Mensaje = $"El número de cedula {request.CedulaEmpleado} no existe" };
            }
            Credito credito = empleado.Creditos.Find(t => t.Numero == request.Numero);
            if (credito != null)
            {
                return new CrearCreditoResponse() { Mensaje = $"El número de credito {request.Numero} ya existe" };

            }
            var errores = CreditBuilder.CanCreateCredit(request.Valor, request.Plazo, request.TasaDeInteres);
            if (errores.Any())
            {
                return new CrearCreditoResponse() { Mensaje = String.Join(",", errores) };
            }
            Credito credritoNuevo = CreditBuilder.CrearCredito(request.Valor, request.Plazo, request.TasaDeInteres);
            credritoNuevo.Numero = request.Numero;
            empleado.Creditos.Add(credritoNuevo);
            _unitOfWork.EmpleadoRepository.Edit(empleado);
            _unitOfWork.Commit();
            return new CrearCreditoResponse() { Mensaje = $"El valor a total para el crédito es ${credritoNuevo.ValorAPagar}" };
        }
    }
    public class CrearCreditoRequest
    {
        public string CedulaEmpleado { get; set; }
        public string Numero { get; set; }
        public double Valor { get; set; }
        public int Plazo { get; set; }
        public double TasaDeInteres { get; set; }
    }
    public class CrearCreditoResponse
    {
        public string Mensaje { get; set; }
    }
}
