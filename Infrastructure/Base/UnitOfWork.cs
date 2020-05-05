using Domain.Contracts;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        private IEmpleadoRepository _empleadoRepository;
        private ICreditoRepository _creditoRepository;
        private IAbonoCuotaRepository _abonoCuotaRepository;
        private IAbonoRepository _abonoRepository;
        public IEmpleadoRepository EmpleadoRepository { get { return _empleadoRepository ?? (_empleadoRepository = new EmpleadoRepository(_dbContext)); } }
        public ICreditoRepository CreditoRepository { get { return _creditoRepository ?? (_creditoRepository = new CreditoRepository(_dbContext)); } }
        public IAbonoCuotaRepository AbonoCuotaRepository { get { return _abonoCuotaRepository ?? (_abonoCuotaRepository = new AbonoCuotaRepository(_dbContext)); } }
        public IAbonoRepository AbonoRepository { get { return _abonoRepository ?? (_abonoRepository = new AbonoRepository(_dbContext)); } }
        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                ((DbContext)_dbContext).Dispose();
                _dbContext = null;
            }
        }

    }
}
