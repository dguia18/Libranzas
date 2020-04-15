using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearEmpleadoService
    {
        readonly IUnitOfWork _unitOfWork;

        public CrearEmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CrearEmpleadoResponse Ejecutar(CrearEmpleadoRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(t => t.Cedula == request.Cedula);
            if (empleado == null)
            {
                Empleado empleadoNuevo = new Empleado();
                empleadoNuevo.Nombre = request.Nombre;
                empleadoNuevo.Cedula = request.Cedula;
                empleadoNuevo.Salario = request.Salario;
                _unitOfWork.EmpleadoRepository.Add(empleadoNuevo);
                _unitOfWork.Commit();
                return new CrearEmpleadoResponse() { Mensaje = $"Se registro con exito el empleado {empleadoNuevo.Nombre}." };
            }
            else
            {
                return new CrearEmpleadoResponse() { Mensaje = $"El empleado con numero de cedula {empleado.Cedula} ya se encuentra registrado" };
            }
        }



    }
    public class CrearEmpleadoRequest
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public double Salario { get; set; }
    }
    public class CrearEmpleadoResponse
    {
        public string Mensaje { get; set; }
    }
}
