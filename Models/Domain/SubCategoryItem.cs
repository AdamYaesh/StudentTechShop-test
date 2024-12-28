namespace StudentTechShop.API.Models.Domain
{
    public class SubCategoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int Status { get; set; }

        public Guid CategoryItemId { get; set; }

        public List<Item> Items { get; set; }

    }
}
