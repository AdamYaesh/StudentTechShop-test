using AutoMapper;
using StudentTechShop.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class CategoryProjectView
    {
        public CategoryProjectView()
        {

        }
        public CategoryProjectView(CategoryProject category)
        {
            Name = category.Name;
            // Description = category.Description;
            Projects = category.Projects?.Select(p => new ProjectView(p)).ToList();

        }


        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<ProjectView> Projects { get; set; }
    }
}

