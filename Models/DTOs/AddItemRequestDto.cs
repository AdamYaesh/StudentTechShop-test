using StudentTechShop.API.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentTechShop.API.Models.DTOs
{
    public class AddItemRequestDto
    {
        [Required]
        //[MinLength(3, ErrorMessage = "you must enter the minimum 3 characters")]
        //[MaxLength(100, ErrorMessage = "you must enter the maximum 100 characters")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public int TimesUsed { get; set; }

        public int Count { get; set; }

        public string? ItemImageUrl { get; set; }

        public Guid? CategoryItemId { get; set; }

        public Guid? SubCategoryItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public string UserId { get; set; }
    }
}
