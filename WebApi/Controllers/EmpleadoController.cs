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

        //Se Recomienda solo dejar la Unidad de Trabajo
        public EmpleadoController(LibranzasContext context, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost("")]
        public ActionResult<Response> Post(CrearEmpleadoRequest request)
        {
            EmpleadoService _service = new EmpleadoService(_unitOfWork);
            Response response = _service.CrearEmpleado(request);
            return Ok(response);
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Empleado>> GetAll()
        {            
            var res = _unitOfWork.EmpleadoRepository.FindBy(includeProperties: "Creditos"); 
            return res.ToList();
        }
    }
}