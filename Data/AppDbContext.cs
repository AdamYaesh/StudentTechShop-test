using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentTechShop.API.Models.Domain;
using System.Reflection.Emit;

namespace StudentTechShop.API.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<CategoryItem> CategoriesItem { get; set; }
        public DbSet<SubCategoryItem> SubCategoriesItem { get; set; }
        public DbSet<CategoryProject> CategoriesProject { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Item>()
               .HasOne(i => i.Project)
               .WithMany(p => p.Items)
               .HasForeignKey(i => i.ProjectId)
               .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            // Project -> User
            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> User
            builder.Entity<Item>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            var userRoleId = "14c9debd-6e99-415a-9958-c99af62c1882";
            var adminRoleId = "3b5c55e2-1c1e-4bde-a526-7a6b2d7ea5e1";
            var adminUserId = "8d5a2de4-31e6-4c59-b2d2-bf71df4e837e";

            // Seed roles
            var roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Id = userRoleId,
            ConcurrencyStamp = userRoleId,
            Name = "User",
            NormalizedName = "USER"
        },
      
        new IdentityRole
        {
            Id = adminRoleId,
            ConcurrencyStamp = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        }
    };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed admin user
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                FirstName="admin",
                IBAN="123",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@Gmail.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin@123"), // Replace with a secure password
                SecurityStamp = Guid.NewGuid().ToString()
            };

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign admin user to admin role
            var adminUserRole = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);


            // Seed CategoriesItem
            var category1 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Motors",
            };

            var category2 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Arduino",
               // Description = "Clothing and apparel",
            };

            var category3 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Tyres",
            };

            var category4 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "ESP",
            };

            var category5 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Raspberry",
            };

            var category6 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Sensors",
            };

            var category7 = new CategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Drivers",
            };

            // Seed SubCategoriesItem
            var subCategory1 = new SubCategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "DC Motors",
              //  Description = "Smartphones and feature phones",
                CategoryItemId = category1.Id
            };

            var subCategory2 = new SubCategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Stepper Motors",
                // Description = "Personal computers",
                CategoryItemId = category1.Id
            };

            var subCategory3 = new SubCategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Servo Motors",
                // Description = "Personal computers",
                CategoryItemId = category1.Id
            };

            var subCategory4 = new SubCategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Arduino Mega",
                // Description = "",
                CategoryItemId = category2.Id
            };

            var subCategory5 = new SubCategoryItem
            {
                Id = Guid.NewGuid(),
                Name = "Arduino Uno",
                // Description = "",
                CategoryItemId = category2.Id
            };

            // Configure data for Categories and Subcategories
            builder.Entity<CategoryItem>().HasData(category1, category2, category3, category4, category5, category6, category7);
            builder.Entity<SubCategoryItem>().HasData(subCategory1, subCategory2, subCategory3, subCategory4, subCategory5);


            // Seed CategoriesProject
            var categoryItem1 = new CategoryProject
            {
                Id = Guid.NewGuid(),
                Name = "Robotics",
             //   Description = "Projects related to building and programming robots"
            };

            var categoryItem2 = new CategoryProject
            {
                Id = Guid.NewGuid(),
                Name = "Image Processing",
             //   Description = "Projects focused on analyzing and processing images"
            };

            var categoryItem3 = new CategoryProject
            {
                Id = Guid.NewGuid(),
                Name = "Networking",
              //  Description = "Projects dealing with computer networks and communication"
            };

            var categoryItem4 = new CategoryProject
            {
                Id = Guid.NewGuid(),
                Name = "Building Machines",
              //  Description = "Projects involving the design and construction of mechanical systems"
            };

            // Configure the seed data
            builder.Entity<CategoryProject>().HasData(categoryItem1, categoryItem2, categoryItem3, categoryItem4);


        }
      


    }
}
