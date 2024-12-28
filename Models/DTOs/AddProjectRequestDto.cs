using StudentTechShop.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class AddProjectRequestDto
    {
        [Required]
        //[MinLength(3, ErrorMessage = "you must enter the minimum 3 characters")]
        //[MaxLength(100, ErrorMessage = "you must enter the maximum 100 characters")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public double Cost { get; set; }

        public string? ProjectImageUrl { get; set; }

        public Guid? CategoryProjectId { get; set; }

        public string UserId { get; set; }

        public List<ItemDto> Items { get; set; }

    }
}
