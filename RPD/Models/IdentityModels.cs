using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RPD.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationUser : IdentityUser
    {

        public List<int> Favorites { get; set; } //match RIds
        public int MyPantry { get; set; } //match pantry PId
        public List<Tag> PrefTags { get; set; } //sets preffered tags
        public List<string> FaveCuisines { get; set; } //sets favorite strings
        public int CaloriePref { get; set; } //sets preference for calories.
        public List<int> Allergy_ids { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var authenticationType = "Basic";
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this.ToString()), authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}