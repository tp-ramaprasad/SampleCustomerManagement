using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pinewood.Customers.Core.Entities;

namespace Pinewood.Customers.Infrastructure
{
    public class CustomerDbContext : IdentityDbContext  //DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> contextOptions) : base(contextOptions)
        {
            PasswordHasher = new PasswordHasher<ApplicationUser>();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Gender>().ToTable("Gender");

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Country)
                .WithMany(c => c.Addresses)                
                .HasForeignKey(a => a.CountryId)
                .IsRequired(false);


            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Gender)
                .WithMany(c => c.Customer)                
                .HasForeignKey(a => a.GenderId)
                .IsRequired(false);

            SeedGenders(modelBuilder);

            SeedCountries(modelBuilder);

            modelBuilder.Entity<Contact>()
                .HasIndex(u => u.EmailAddress) // Specify the property
                .IsUnique(); // Set it as unique

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");

            SeedApplicationUsers(modelBuilder);
            //modelBuilder.Entity<User>().ToTable("User");
            //SeedUsers(modelBuilder);
        }

        private static void SeedGenders(ModelBuilder modelBuilder)
        {
            var GenderList = new List<Gender>
            {
                new Gender{ Id=1, Name="Male" },
                new Gender{ Id=2,Name="Female"},
                new Gender{ Id=3,Name= "N/A"}
            };

            modelBuilder.Entity<Gender>().HasData(GenderList);
        }

        private static void SeedCountries(ModelBuilder modelBuilder)
        {
            var countries = new List<Country>
            {
                new Country{ Id=1, Name="United Kingdom" }
            };

            modelBuilder.Entity<Country>().HasData(countries);
        }

        /// <summary>
        /// this is only for adding a admin user who can create a other customers.this will be useful if we add a login page
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedApplicationUsers(ModelBuilder modelBuilder)
        {
            var username = "admin@pinewooddms.com";
            var email = "admin@pinewooddms.com";
            var password = "admin!420";

            var user = new ApplicationUser
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                Role = Role.Admin
            };
            user.PasswordHash = PasswordHasher.HashPassword(user, password);

            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }


        ///// <summary>
        ///// this is only for adding a admin user who can create a other customers.this will be useful if we add a login page
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //private void SeedUsers(ModelBuilder modelBuilder)
        //{
        //    var username = "admin@pinewooddms.com";
        //    var email = "admin@pinewooddms.com";
        //    var password = "admin!420";

        //    var user = new User
        //    {
        //        Id = 1,
        //        Username = username,
        //        FirstName = "admin",
        //        LastName = "admin",
        //        Email = email,
        //        EmailConfirmed = true,
        //        LockoutEnabled = false,
        //        Role = Role.Admin,
        //    };

        //    user.PasswordHash = PasswordHasher.HashPassword(user, password);
        //    modelBuilder.Entity<User>().HasData(user);
        //}


        private PasswordHasher<ApplicationUser> PasswordHasher { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<Preference> Preference { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<Country> Country { get; set; }

    }
}
