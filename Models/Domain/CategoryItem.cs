namespace StudentTechShop.API.Models.Domain
{
    public class CategoryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public List<Item> Items { get; set; }

        public List<SubCategoryItem> SubCategoriesItem { get; set; }



    }
}
