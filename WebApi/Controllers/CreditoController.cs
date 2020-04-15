using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        readonly LibranzasContext _context;
        readonly IUnitOfWork _unitOfWork;
        public CreditoController(LibranzasContext context,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
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
            UnitOfWork unitOfWork = new UnitOfWork(_context);
            var res = unitOfWork.CreditoRepository.Get(t=> t.Id == id);
            return res.ToList();
        }
    }
}