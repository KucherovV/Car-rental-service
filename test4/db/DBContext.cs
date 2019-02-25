using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace db
{
    public class DBContext : IdentityDbContext
    {
        public class ApplicationUser : IdentityUser
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }

        public DBContext(): base("DefaultConnection") { }

        public DbSet<Car> Cars { get; set; }
    }
}
