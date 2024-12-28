using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Models.DTOs
{
    public class ProjectDto
    {
      //  public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public double Cost { get; set; }

        public string? ProjectImageUrl { get; set; }

        public string UserId { get; set; } // مفتاح خارجي

        public UserView User { get; set; } // كائن الناشر


        public List<ItemDto> Items { get; set; }
    }


    public class ProjectView
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Cost { get; set; }

        public string UserId { get; set; } // مفتاح خارجي

        public string? ProjectImageUrl { get; set; }

        public ProjectView() { }

        public ProjectView(Project project)
        {
            Name = project.Name;
            Cost = project.Cost;
            Description = project.Description;
            ProjectImageUrl = project.ProjectImageUrl;
        }
    }
}
