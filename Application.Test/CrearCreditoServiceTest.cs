using Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Application.Test
{
    class CrearCreditoServiceTest
    {
        LibranzasContext _context;
        [SetUp]
        public void SetUp()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<LibranzasContext>().UseInMemoryDatabase("Libranzas").Options;

            _context = new LibranzasContext(optionsInMemory);
        }
        private IEnumerable TestData()
        {
            yield return new TestCaseData()  
        }
    }
}
