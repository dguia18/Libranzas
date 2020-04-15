using Domain.Contracts;
using Domain.Entities;
using System;

namespace Application
{
    public class AbonarService
    {
        readonly IUnitOfWork _unitOfWork;

        public AbonarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AbonarResponse Ejecutar(AbonarRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(t => t.Cedula == request.CedulaEmpleado);
            if (empleado != null)
            {
                Credito credito = empleado.Creditos.Find(t => t.Numero == request.NumeroCrediro);
                if (credito != null)
                {
                    var errores = credito.CanAbonar(request.Valor);
                    if (errores.Count == 0)
                    {
                        string mensaje = credito.Abonar(request.Valor);
                        _unitOfWork.Commit();
                        return new AbonarResponse() { Mensaje = mensaje };
                    }
                    else
                    {
                        return new AbonarResponse() { Mensaje = String.Join(",",errores) };
                    }
                }
                else
                {
                    return new AbonarResponse() { Mensaje = $"Señor {empleado.Nombre}, hasta el momento no tiene un credito de numero {request.NumeroCrediro}" };
                }
            }
            else
            {
                return new AbonarResponse() { Mensaje = $"El empleado con cedula {request.CedulaEmpleado} no se encuentra registrado en el sistema" };
            }
        }
    }
    public class AbonarRequest
    {
        public string CedulaEmpleado { get; set; }
        public string NumeroCrediro { get; set; }
        public double Valor { get; set; }
    }
    public class AbonarResponse
    {
        public string Mensaje { get; set; }
    }
}
