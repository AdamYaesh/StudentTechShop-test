using Microsoft.AspNetCore.Identity;

namespace StudentTechShop.API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? IBAN { get; set; }

        public int Status { get; set; }

        // يمكن للمستخدم أن يمتلك قطعًا كبائع
        public List<Item> Items { get; set; } = new List<Item>();

        public List<Project> Projects { get; set; } = new List<Project>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Purchase> Purchases { get; set; } // Items the user has purchased



    }
}
