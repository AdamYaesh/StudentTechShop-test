namespace StudentTechShop.API.Models.Domain
{
    public class CategoryProject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public List<Project> Projects { get; set; }
    }
}
