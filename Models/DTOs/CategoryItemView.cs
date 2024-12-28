using StudentTechShop.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class CategoryItemView
    {

        public CategoryItemView()
        {

        }
        public CategoryItemView(CategoryItem category)
        {
            Name = category.Name;
            Description = category.Description;
            SubCategories = category.SubCategoriesItem.Select(x => new SubCategoryItemView(x)).ToList();
            Items = category.Items?.Select(p => new ItemView(p)).ToList();

        }


        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<SubCategoryItemView> SubCategories { get; set; }

        public List<ItemView> Items { get; set; }
    }


    public class SubCategoryItemView
    {
        public SubCategoryItemView()
        {

        }
        public SubCategoryItemView(SubCategoryItem subCategory)
        {
            Name = subCategory.Name;
            Description = subCategory.Description;
            Items = subCategory.Items?.Select(p => new ItemView(p)).ToList();

        }
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<ItemView> Items { get; set; }

    }
}
