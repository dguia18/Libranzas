using Domain.Contracts;
using Domain.Entities;
using System;
using System.Linq;

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
			Empleado empleado = _unitOfWork.EmpleadoRepository.
				FindBy(filter: t => t.Cedula == request.CedulaEmpleado, includeProperties: "Creditos").FirstOrDefault();
			if (empleado == null)
			{
				return new AbonarResponse() { Mensaje = $"El empleado con cedula {request.CedulaEmpleado} no se encuentra registrado en el sistema" };

			}
			Credito credito = _unitOfWork.CreditoRepository.FindBy(t => t.Numero == request.NumeroCredito, includeProperties: "Cuotas,Abonos").FirstOrDefault();
			if (credito == null)
			{
				return new AbonarResponse() { Mensaje = $"hasta el momento no tiene un credito de numero {request.NumeroCredito}" };
			}
			var errores = credito.CanAbonar(request.Valor);
			if (errores.Count != 0)
			{
				return new AbonarResponse() { Mensaje = String.Join(",", errores) };
			}
			string mensaje = credito.Abonar(request.Valor);
			_unitOfWork.Commit();
			return new AbonarResponse() { Mensaje = mensaje };
		}
	}
	public class AbonarRequest
	{
		public string CedulaEmpleado { get; set; }
		public string NumeroCredito { get; set; }
		public double Valor { get; set; }
	}
	public class AbonarResponse
	{
		public string Mensaje { get; set; }
	}
}
