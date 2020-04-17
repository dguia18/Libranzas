using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Base;

namespace Infrastructure.Repositories
{
    public class CreditoRepository : GenericRepository<Credito>, ICreditoRepository
    {
        public CreditoRepository(IDbContext context) : base(context)

        {

        }
    }
}
