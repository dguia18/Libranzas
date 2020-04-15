using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
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
            if (empleado != null)
            {
                Credito credito = empleado.Creditos.Find(t => t.Numero == request.Numero);
                if (credito == null)
                {
                    var errores = CreditBuilder.CanCreateCredit(request.Valor, request.Plazo, request.TasaDeInteres);
                    if (errores.Count == 0)
                    {
                        Credito credritoNuevo = CreditBuilder.CrearCredito(request.Valor, request.Plazo, request.TasaDeInteres);
                        credritoNuevo.Numero = request.Numero;
                        empleado.Creditos.Add(credritoNuevo);
                        _unitOfWork.EmpleadoRepository.Edit(empleado);
                        _unitOfWork.Commit();
                        return new CrearCreditoResponse() { Mensaje = $"El valor a total para el crédito es ${credritoNuevo.ValorAPagar}" };
                    }
                    else
                    {
                        return new CrearCreditoResponse() { Mensaje = String.Join(",", errores) };
                    }
                }
                else
                {
                    return new CrearCreditoResponse() { Mensaje = $"El número de credito {request.Numero} ya existe" };
                }
            }
            else
            {
                return new CrearCreditoResponse() { Mensaje = $"El número de cedula {request.CedulaEmpleado} no existe" };
            }
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
