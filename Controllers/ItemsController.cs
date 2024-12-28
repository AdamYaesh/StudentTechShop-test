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
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IItemRepository itemRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(AppDbContext dbContext,
            IItemRepository itemRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.itemRepository = itemRepository;
            this.mapper = mapper;
        }


        // Get: /api/Items?filterOn=Name&filterQuery="" &sortBy=Name&isAscending=true &pageNumber=1&pageSize=10
        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {


            // Get Data From Database - Domain Models
            var itemsDomain = await itemRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true);

            // Map Domain Models to DTOs

            /*var itemsDto= new List<ItemDto>();
            foreach (var itemDomain in itemsDomain)
            {
                itemsDto.Add(new ItemDto()
                {
                    Id = itemDomain.Id,
                    Name = itemDomain.Name,
                    Description = itemDomain.Description,
                    Price = itemDomain.Price,
                    ItemImageUrl = itemDomain.ItemImageUrl
                });
            }*/


            // Map Domain Models to DTOs
            //Return DTOs 
            var username = HttpContext.Items["Username"]?.ToString();


            return Ok(mapper.Map<List<ItemDto>>(itemsDomain));
        }



        [HttpGet]
        [Route("{id:Guid}")]
      //  [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var item = dbContext.Items.Find(id);
            // Get Item Domain Model  From Database
            var itemDomain = await itemRepository.GetByIdAsync(id);

            if (itemDomain == null)
            {
                return NotFound();
            }

            // Map Item Domain Model to Item DTO

            /*var itemDto = new ItemDto
            {
                Id = itemDomain.Id,
                Name = itemDomain.Name,
                Description = itemDomain.Description,
                Price = itemDomain.Price,
                ItemImageUrl = itemDomain.ItemImageUrl
            };*/


            // Map Item Domain Model to Item DTO
            // Return DTO back to client
            return Ok(mapper.Map<ItemDto>(itemDomain));

        }


        [HttpGet("{name}")]
       // [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetByName(string name)
        {
          
            var itemsDomain = await itemRepository.GetByNameAsync(name);

            if (itemsDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ItemDto>>(itemsDomain));

        }


        [HttpPost]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddItemRequestDto addItemRequestDto)
        {
            // Map DTO to Doman Model
            /*var itemDomain = new Item
            {
                Name = addItemRequestDto.Name,
                Description = addItemRequestDto.Description,
                Price = addItemRequestDto.Price,
                ItemImageUrl = addItemRequestDto.ItemImageUrl
            };*/

            // Map DTO to Doman Model
            var itemDomain = mapper.Map<Item>(addItemRequestDto);

            // Use Domain Model to create Item
            itemDomain = await itemRepository.CreateAsync(itemDomain);

            // Map Domain Model back to DTO
            /*var itemDto = new ItemDto
             {
                 Id = addItemRequestDto.Id,
                 Name = addItemRequestDto.Name,
                 Description = addItemRequestDto.Description,
                 Price = addItemRequestDto.Price,
                 ItemImageUrl = addItemRequestDto.ItemImageUrl

             };*/


            // Map Domain Model back to DTO
            var itemDto = mapper.Map<ItemDto>(itemDomain);

            return CreatedAtAction(nameof(GetById), new { id = itemDomain.Id }, itemDto);
        }



        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateItemRequestDto updateItemRequestDto)
        {
            // Map DTO to Domain Model
            /*var itemDomain = new Item
            {
                Name = updateItemRequestDto.Name,
                Description = updateItemRequestDto.Description,
                Price = updateItemRequestDto.Price,
                ItemImageUrl = updateItemRequestDto.ItemImageUrl

            };*/

            // Map DTO to Domain Model
            var itemDomain = mapper.Map<Item>(updateItemRequestDto);

            // Check if user exists
            itemDomain = await itemRepository.UpdateAsync(id, itemDomain);


            if (itemDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Domain Model to DTO 
            /*var itemDto = new ItemDto
            {
                Id = itemDomain.Id,
                Name = itemDomain.Name,
                Description = itemDomain.Description,
                Price = itemDomain.Price,
                ItemImageUrl = itemDomain.ItemImageUrl

            };*/

            // Map/Convert Domain Model to DTO 
            //var itemDto = mapper.Map<ItemDto>(itemDomain);

            return Ok(mapper.Map<ItemDto>(itemDomain));
        }


        [HttpDelete]
        [Route("{id:Guid}")]
    //    [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var itemDomain = await itemRepository.DeleteAsync(id);

            if (itemDomain == null)
            {
                return NotFound();
            }

            // return deleted item back
            // Map Domain Model to DTO
            /*var itemDto = new itemDto
            {
                Id = itemDomain.Id,
                Name = itemDomain.Name,
                Description = itemDomain.Description,
                Price = itemDomain.Price,
                ItemImageUrl = itemDomain.ItemImageUrl

            };*/

            // Map Domain Model to DTO
            //var itemDto = mapper.Map<ItemDto>(itemDomain);

            return Ok(mapper.Map<ItemDto>(itemDomain));
        }
    }
}
