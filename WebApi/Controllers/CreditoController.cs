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
		private readonly IUnitOfWork _unitOfWork;
		readonly CreditoService CreditoService;
		public CreditoController(LibranzasContext context, IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_context = context;
			CreditoService = new CreditoService(_unitOfWork);
		}
		[HttpPost("")]
		public ActionResult<Response> Post(CrearCreditoRequest request)
		{			
			Response response = CreditoService.CrearCredito(request);
			return Ok(response);
		}
		[HttpPost("Abonar")]
		public ActionResult<Response> Post(AbonarRequest request)
		{			
			var response = CreditoService.Abonar(request);
			return Ok(response);
		}
		[HttpGet]
		public ActionResult<IEnumerable<Credito>> GetAll()
		{			
			return CreditoService.GetCreditos().ToList();
		}
		[HttpGet("{numero}/cuotas")]
		public ActionResult<Credito> GetCuotas(string numero)
		{
			return Ok(CreditoService.GetCredito(numero,"Cuotas"));
		}
		[HttpGet("{numero}/abonos")]
		public ActionResult<Credito> GetAbonos(string numero)
		{
			return Ok(CreditoService.GetCredito(numero,"Abonos"));
		}
		[HttpGet("abonos")]
		public ActionResult<IEnumerable<Abono>> GetAbonoCuotas()
		{
			return Ok(CreditoService.GetAbonoCuotas());
		}
	}
}