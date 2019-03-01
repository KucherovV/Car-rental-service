using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IDNumber { get; set; }

        public string TaxID { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DrivingLicenseDate { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime? BlockEnd { get; set; }
    }
}
