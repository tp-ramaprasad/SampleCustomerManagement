using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;

namespace Pinewood.Customers.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
    {

    }
}
