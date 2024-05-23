using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;

namespace Pinewood.Customers.Infrastructure.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(CustomerDbContext dbContext) : base(dbContext)
        {

        }
    }
}
