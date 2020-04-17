using Application.Base;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class CreditoService
    {
        readonly IUnitOfWork _unitOfWork;
        EmpleadoService empleadoService;
        public CreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            empleadoService = new EmpleadoService(_unitOfWork);
        }
        public Response CrearCredito(CrearCreditoRequest request)
        {
            Empleado empleado = empleadoService.GetEmpleado(request.CedulaEmpleado);
            if (empleado == null)
            {
                return new Response() { Mensaje = $"El número de cedula {request.CedulaEmpleado} no existe" };
            }
            Credito credito = GetCredito(request.Numero);
            if (credito != null)
            {
                return new Response() { Mensaje = $"El número de credito {request.Numero} ya existe" };

            }
            var errores = CreditBuilder.CanCreateCredit(request.Valor, request.Plazo, request.TasaDeInteres);
            if (errores.Any())
            {
                return new Response() { Mensaje = String.Join(",", errores) };
            }
            Credito credritoNuevo = CreditBuilder.CrearCredito(request.Valor, request.Plazo, request.TasaDeInteres);
            credritoNuevo.Numero = request.Numero;
            empleado.Creditos.Add(credritoNuevo);
            _unitOfWork.EmpleadoRepository.Edit(empleado);
            _unitOfWork.Commit();
            return new Response() { Mensaje = $"El valor a total para el crédito es ${credritoNuevo.ValorAPagar}" };
        }
        public Response Abonar(AbonarRequest request)
        {
            Empleado empleado = empleadoService.GetEmpleado(request.CedulaEmpleado);
            if (empleado == null)
            {
                return new Response() { Mensaje = $"El empleado con cedula {request.CedulaEmpleado} no se encuentra registrado en el sistema" };
            }
            Credito credito = GetCredito(request.NumeroCredito);
            if (credito == null)
            {
                return new Response() { Mensaje = $"Señor {empleado.Nombre}, hasta el momento no tiene un credito de numero {request.NumeroCredito}" };
            }
            var errores = credito.CanAbonar(request.Valor);
            if (errores.Count != 0)
            {
                return new Response() { Mensaje = String.Join(",", errores) };
            }
            string mensaje = credito.Abonar(request.Valor);
            _unitOfWork.Commit();
            return new Response() { Mensaje = mensaje };
        }        
        public Credito GetCredito(string numero)
        {
            return _unitOfWork.CreditoRepository.
                FindBy(t => t.Numero == numero, includeProperties: "Cuotas,Abonos").FirstOrDefault();
        }
        public IEnumerable<Credito> GetCreditos()
        {
            return _unitOfWork.CreditoRepository.FindBy(includeProperties: "Cuotas,Abonos");
        }
        public IEnumerable<AbonoCuota> GetAbonoCuotas()
        {
            return _unitOfWork.AbonoCuotaRepository.FindBy(includeProperties: "Abonos,Cuotas");
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
    public class AbonarRequest
    {
        public string CedulaEmpleado { get; set; }
        public string NumeroCredito { get; set; }
        public double Valor { get; set; }
    }
}
