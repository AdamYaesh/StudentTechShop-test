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
    public class PurchaseController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public PurchaseController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // POST: api/Purchase/Received

        [HttpPost("Received/{id:guid}")]
        public async Task<IActionResult> Received(Guid id)
        {
            var purchase = await dbContext.Purchases
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound("Item not found.");
            }

            purchase.Received = true;
            purchase.ReceivedDate = DateTime.Now;

            await dbContext.SaveChangesAsync();


            return Ok("Purchase Received successfully.");

        }


        // POST: api/Purchase
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            if (purchaseDto == null)
            {
                return BadRequest("Invalid purchase data.");
            }


            var item = await dbContext.Items.FirstOrDefaultAsync(p => p.Id == purchaseDto.ItemId);

            if (item == null)
            {
                return NotFound("Item not found.");

            }

            if (item.Count == 0)
            {
                return BadRequest("The item has been sold.");

            }

            if (purchaseDto.Quantity > item.Count)
            {
                return BadRequest("Not enough items.");

            }

            var purchase = new Purchase
            {
                Id = Guid.NewGuid(),
                PurchaseDate = DateTime.UtcNow,
                Received = false,
                Status = 1,
                Quantity = purchaseDto.Quantity,
              //  TotalPrice = purchaseDto.TotalPrice,
                ItemId = purchaseDto.ItemId,
                UserId = purchaseDto.UserId
            };

            purchase.TotalPrice = item.Price * purchase.Quantity;


            dbContext.Purchases.Add(purchase);
          //  await dbContext.SaveChangesAsync();

            item.Count -= purchaseDto.Quantity;

            await dbContext.SaveChangesAsync();

            // Map to DTO
            var purchaseResponseDto = new PurchaseResponseDto
            {
                PurchaseDate = purchase.PurchaseDate,
                TotalPrice = purchase.TotalPrice,
                Quantity = purchase.Quantity,
                ItemId = purchase.ItemId,
                UserId = purchase.UserId
            };

            return Ok(purchaseResponseDto);
        }

        // GET: api/Purchase
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetAllPurchases(string userId)
        {
            var purchases = await dbContext.Purchases
                .Include(p => p.Item)
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToListAsync();



           // var purchaseViews = mapper.Map<PurchaseView>(purchases);
           // var purchaseViews = mapper.Map<List<PurchaseView>>(purchases); // Map collection


            var purchaseDtos = purchases.Select(p => new
            {
                p.Id,
                p.PurchaseDate,
                p.Quantity,
                p.TotalPrice,
                Item = new { p.Item.Id, p.Item.Name, p.Item.Count, p.Item.ItemImageUrl, p.Item.Price },
                User = new { p.User.Id, p.User.UserName, p.User.Email }
            });

            return Ok(purchaseDtos);
        }

        // GET: api/Purchase/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPurchaseById(Guid id)
        {
            var purchase = await dbContext.Purchases
                .Include(p => p.Item)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound("Purchase not found.");
            }

            var purchaseView = mapper.Map<PurchaseView>(purchase);
           /* var purchaseDto = new
            {
                purchase.Id,
                // purchase.PurchaseDate,
                purchase.Quantity,
                purchase.TotalPrice,
                Item = new { purchase.Item.Id, purchase.Item.Name, purchase.Item.Price },
                User = new { purchase.User.Id, purchase.User.UserName, purchase.User.Email }
            };*/

            return Ok(purchaseView);
        }

        // PUT: api/Purchase/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePurchase(Guid id, [FromBody] UpdatePurchase updatePurchase)
        {
            var purchase = await dbContext.Purchases.FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound("Purchase not found.");
            }

            purchase.Quantity = updatePurchase.Quantity;
            purchase.TotalPrice = updatePurchase.TotalPrice;
          //  purchase.PurchaseDate = purchaseDto.PurchaseDate;

            await dbContext.SaveChangesAsync();

            return Ok("Purchase updated successfully.");
        }

        // DELETE: api/Purchase/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePurchase(Guid id)
        {
            var purchase = await dbContext.Purchases.FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound("Purchase not found.");
            }

            purchase.Status = 2;

            dbContext.Purchases.Remove(purchase);
            await dbContext.SaveChangesAsync();

            return Ok("Purchase deleted successfully.");
        }
    }
}
