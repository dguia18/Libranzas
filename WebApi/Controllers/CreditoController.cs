using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Base;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreditoController : ControllerBase
	{
		readonly LibranzasContext _context;
		readonly IUnitOfWork _unitOfWork;
		public CreditoController(LibranzasContext context, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_context = context;
		}
		[HttpPost("")]
		public ActionResult<Response> Post(CrearCreditoRequest request)
		{
			CreditoService _service = new CreditoService(_unitOfWork);
			Response response = _service.CrearCredito(request);
			return Ok(response);
		}
		[HttpPost("Abonar")]
		public ActionResult<Response> Post(AbonarRequest request)
		{
			CreditoService _service = new CreditoService(_unitOfWork);
			var response = _service.Abonar(request);
			return Ok(response);
		}
		[HttpGet]
		public ActionResult<IEnumerable<Credito>> GetAll()
		{
			UnitOfWork unitOfWork = new UnitOfWork(_context);
			var res = unitOfWork.CreditoRepository.GetAll();
			return res.ToList();
		}
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<Credito>> Get(long id)
		{
			
			var res = _unitOfWork.CreditoRepository.FindBy(t => t.Id == id);
			return res.ToList();
		}
	}
}