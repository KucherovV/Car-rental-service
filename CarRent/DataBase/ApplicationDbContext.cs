using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Entities;

namespace DataBase
{
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        return userIdentity;
    //    }

    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string IDNumber { get; set; }
    //    public string TaxID { get; set; }
    //    public DateTime BirthDate { get; set; }
    //    public DateTime DrivingLicenseDate { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<AdditionalOption> AdditionalOptions { get; set; }
        public DbSet<Blocking> Blockings { get; set; }
        public DbSet<CarTimePricing> CarTimePricings { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderConfirmDeny> OrderConfirmDenies { get; set; }
        public DbSet<OrderProblem> OrderProblems { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<CarRent.ViewModels.ManagerOrderViewModel> ManagerOrderViewModels { get; set; }

        //public System.Data.Entity.DbSet<Entities.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<Entities.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<Entities.ApplicationUser> ApplicationUsers { get; set; }
    }
}


