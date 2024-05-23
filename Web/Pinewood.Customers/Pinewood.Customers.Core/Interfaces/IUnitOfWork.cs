

namespace Pinewood.Customers.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IGenderRepository Genders { get; }
        ICountryRepository Countries { get; }
        Task<int> SaveAsync();
        IUserRepository Users { get; }
    }
}
