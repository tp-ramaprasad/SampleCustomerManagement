using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;

namespace Pinewood.Customers.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }       

    }
}
