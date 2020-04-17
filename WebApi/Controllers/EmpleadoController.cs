using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Base;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        readonly LibranzasContext _context;
        readonly IUnitOfWork _unitOfWork;
        readonly EmpleadoService empleadoService;

        //Se Recomienda solo dejar la Unidad de Trabajo
        public EmpleadoController(LibranzasContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            empleadoService = new EmpleadoService(_unitOfWork);
        }

        [HttpPost("")]
        public ActionResult<Response> Post(CrearEmpleadoRequest request)
        {
            Response response = empleadoService.CrearEmpleado(request);
            return Ok(response);
        }
        [HttpGet("{cedula}")]
        public ActionResult<Credito> Get(string cedula)
        {
            return Ok(empleadoService.GetEmpleado(cedula));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Empleado>> GetAll()
        {                        
            return empleadoService.GetEmpleados().ToList();
        }
    }
}