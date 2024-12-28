namespace StudentTechShop.API.Models.Domain
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
        
        public double Cost { get; set; }

        public string? ProjectImageUrl { get; set; }

        public int Status { get; set; }

        public Guid? CategoryProjectId { get; set; } // مفتاح خارجي

        public CategoryProject CategoryProject { get; set; }

        public List<Item> Items { get; set; }




        public string UserId { get; set; } // مفتاح خارجي
        public ApplicationUser User { get; set; } // كائن الناشر
    }
}
