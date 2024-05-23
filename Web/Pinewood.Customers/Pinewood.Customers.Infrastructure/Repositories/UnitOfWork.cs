using Pinewood.Customers.Core.Interfaces;

namespace Pinewood.Customers.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext dbContext;

        public ICustomerRepository Customers { get; }

        public IGenderRepository Genders { get; }

        public ICountryRepository Countries { get; }

        public IUserRepository Users { get; }

        public UnitOfWork(CustomerDbContext dbContext,
                            ICustomerRepository customerRepository,
                            IGenderRepository genderRepository,
                            ICountryRepository countryRepository,
                            IUserRepository usersRepository)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Customers = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            Genders = genderRepository ?? throw new ArgumentNullException(nameof(genderRepository));
            Countries = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            Users = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

    }
}
