using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;

namespace Pinewood.Customers.Infrastructure.Repositories
{
    public class GenderRepository : GenericRepository<Gender>, IGenderRepository
    {
        public GenderRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
