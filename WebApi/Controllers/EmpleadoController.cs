using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("CrearEmpleado")]
        public ActionResult<CrearEmpleadoResponse> Post(CrearEmpleadoRequest request)
        {
            CrearEmpleadoService _service = new CrearEmpleadoService(_unitOfWork);
            CrearEmpleadoResponse response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpPost("CrearCredito")]
        public ActionResult<CrearCreditoResponse> Post(CrearCreditoRequest request)
        {
            CrearCreditoService _service = new CrearCreditoService(_unitOfWork);
            CrearCreditoResponse response = _service.Ejecutar(request);
            return Ok(response);
        }

        [HttpPost("Abonar")]
        public ActionResult<AbonarResponse> Post(AbonarRequest request)
        {
            var _service = new AbonarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            return Ok(response);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Empleado>> GetAll()
        {
            
            var res = _unitOfWork.EmpleadoRepository.Get(includeProperties: "Creditos"); 
            return res.ToList();
        }
    }
}