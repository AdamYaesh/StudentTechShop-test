using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class UpdateProjectRequestDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Cost { get; set; }

        public string? ProjectImageUrl { get; set; }

      //  public Guid? CategoryProjectId { get; set; } // مفتاح خارجي

    }
}
