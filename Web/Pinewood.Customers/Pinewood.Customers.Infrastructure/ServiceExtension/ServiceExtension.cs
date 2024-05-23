using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;
using Pinewood.Customers.Infrastructure.Repositories;
using System.Data.Common;

namespace Pinewood.Customers.Infrastructure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {

            // This in-memory implementation is acceptable for now           
            services.AddDbContext<CustomerDbContext>(options =>
            {
                static DbConnection CreateInMemoryDatabase()
                {
                    var connection = new SqliteConnection("Filename=Customers.db");
                    
                    return connection;
                }
                options.UseLazyLoadingProxies();
                options.UseSqlite(CreateInMemoryDatabase());                
            });

            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<CustomerDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();      
            return services;
        }
    }
}
