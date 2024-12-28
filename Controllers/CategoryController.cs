using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTechShop.API.CustomActionFilters;
using StudentTechShop.API.Data;
using StudentTechShop.API.Models.Domain;
using StudentTechShop.API.Models.DTOs;
using StudentTechShop.API.Repositories;
using System.Text.Json;

namespace StudentTechShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryController(AppDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        [HttpGet("CategoryItem/All")]
        public async Task<IActionResult> GetAllCategoryItem()
        {

            var categories = dbContext.CategoriesItem.Include(x => x.SubCategoriesItem).ToList();
            var categoryViews = categories.Select(x => new CategoryItemView(x)).ToList();


            return Ok(categoryViews);
        }


        [HttpGet("CategoryItem/{id}")]
        public async Task<IActionResult> GetCategoryItemById(Guid id)
        {
            // Retrieve the category by id and include its subcategories and items
            var categoryItem = await dbContext.CategoriesItem
                .Include(c => c.SubCategoriesItem) // Include the related SubCategoryItems
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoryItem == null)
            {
                return NotFound(new { Message = $"CategoryItem with Id {id} not found" });
            }

            var categoryItemView = new CategoryItemView(categoryItem);
            return Ok(categoryItemView);
        }


        [HttpGet("SubCategoryItem/{id}")]
        public async Task<IActionResult> GetSubCategoryItemById(Guid id)
        {
            // Retrieve the subcategory by id and include its items
            var subcategoryItem = await dbContext.SubCategoriesItem
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (subcategoryItem == null)
            {
                return NotFound(new { Message = $"SubCategoryItem with Id {id} not found" });
            }

            var subcategoryItemView = new SubCategoryItemView(subcategoryItem);
            return Ok(subcategoryItemView);
        }


        [HttpGet("CategoryProject/All")]
        public async Task<IActionResult> GetAllCategoryProject()
        {

            var categories = dbContext.CategoriesProject;
            var categoryViews = categories.Select(x => new CategoryProjectView(x)).ToList();


            return Ok(categoryViews);
        }


        // New Get CategoryProject by Id
        [HttpGet("CategoryProject/{id}")]
        public async Task<IActionResult> GetCategoryProjectById(Guid id)
        {
            var categoryProject = await dbContext.CategoriesProject
                .Include(c => c.Projects) // If you want to include related projects
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoryProject == null)
            {
                return NotFound(new { Message = $"CategoryProject with Id {id} not found" });
            }

            var categoryProjectView = new CategoryProjectView(categoryProject);

            return Ok(categoryProjectView);
        }








    }
}
