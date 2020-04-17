using Application.Base;
using Domain.Contracts;
using Domain.Entities;

namespace Application
{
    public class EmpleadoService
    {
        readonly IUnitOfWork _unitOfWork;

        public EmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Response CrearEmpleado(CrearEmpleadoRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(t => t.Cedula == request.Cedula);
            if (empleado != null)
            {
                return new Response() { Mensaje = $"El empleado con numero de cedula {empleado.Cedula} ya se encuentra registrado" };
            }
            Empleado empleadoNuevo = new Empleado();
            empleadoNuevo.Nombre = request.Nombre;
            empleadoNuevo.Cedula = request.Cedula;
            empleadoNuevo.Salario = request.Salario;
            _unitOfWork.EmpleadoRepository.Add(empleadoNuevo);
            _unitOfWork.Commit();
            return new Response() { Mensaje = $"Se registro con exito el empleado {empleadoNuevo.Nombre}." };

        }



    }
    public class CrearEmpleadoRequest
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public double Salario { get; set; }
    }

}
