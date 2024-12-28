using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(AppDbContext dbContext,
            IProjectRepository projectRepository,
            IMapper mapper,
            ILogger<ProjectsController> logger)
        {
            this.dbContext = dbContext;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        // Get: /api/Projects?filterOn=Name&filterQuery="" &sortBy=Name&isAscending=true &pageNumber=1&pageSize=10
        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {


            // Get Data From Database - Domain Models
            var projectsDomain = await projectRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true);


            // Map Domain Models to DTOs
            //Return DTOs 


            return Ok(mapper.Map<List<ProjectDto>>(projectsDomain));
        }


        [HttpGet]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var project = dbContext.Projects.Find(id);
            // Get Project Domain Model  From Database
            var projectDomain = await projectRepository.GetByIdAsync(id);

            if (projectDomain == null)
            {
                return NotFound();
            }
            var projectDto=mapper.Map<ProjectDto>(projectDomain);
            projectDto.Items = mapper.Map<List<ItemDto>>(projectDomain.Items);


            // Map Project Domain Model to Project DTO
            // Return DTO back to client
            return Ok(mapper.Map<ProjectDto>(projectDomain));

        }


        [HttpGet("{name}")]
        // [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetByName(string name)
        {

            var projectsDomain = await projectRepository.GetByNameAsync(name);

            if (projectsDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProjectDto>>(projectsDomain));

        }


        [HttpPost]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddProjectRequestDto addProjectRequestDto)
        {

            // Map DTO to Doman Model
            var projectDomain = mapper.Map<Project>(addProjectRequestDto);
            projectDomain.Items= mapper.Map<List<Item>>(addProjectRequestDto.Items);

            
            // Use Domain Model to create Item
            projectDomain = await projectRepository.CreateAsync(projectDomain);


            // Map Domain Model back to DTO
            var projectDto = mapper.Map<ProjectDto>(projectDomain);

            return CreatedAtAction(nameof(GetById), new { id = projectDomain.Id }, projectDto);
        }



        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {

            // Map DTO to Domain Model
            var projectDomain = mapper.Map<Project>(updateProjectRequestDto);

            // Check if user exists
            projectDomain = await projectRepository.UpdateAsync(id, projectDomain);


            if (projectDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProjectDto>(projectDomain));
        }


       /* [HttpDelete]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var projectDomain = await projectRepository.DeleteAsync(id);

            if (projectDomain == null)
            {
                return NotFound();
            }

            // return deleted item back

            // Map Domain Model to DTO
            //var itemDto = mapper.Map<ItemDto>(itemDomain);

            return Ok(mapper.Map<ProjectDto>(projectDomain));
        }*/
    }
}
