using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class UpdateItemRequestDto
    {
        [Required]
        //[MinLength(3, ErrorMessage = "you must enter the minimum 3 characters")]
        //[MaxLength(100, ErrorMessage = "you must enter the maximum 100 characters")]
        public string Name { get; set; }
        
      //  public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public int TimesUsed { get; set; }

        public int Count { get; set; }

      //  public Guid? CategoryItemId { get; set; } // مفتاح خارجي

     //   public Guid? SubCategoryItemId { get; set; } // مفتاح خارجي

        public string? ItemImageUrl { get; set; }
    }
}
