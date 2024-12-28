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
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public ReviewController(AppDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // POST: api/Review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] AddReview addReview)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Comment = addReview.Comment,
                Grade = addReview.Grade,
                ItemId = addReview.ItemId,
                UserId = addReview.UserId,
                Status = 1, // Default status
             //   Description = null // Optional, can be set in the future
            };

            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        // GET: api/Review/{itemId:guid}
        [HttpGet("Item/{itemId:guid}")]
        public async Task<IActionResult> GetAllReviews(Guid itemId)
        {
            var reviews = await dbContext.Reviews
                .Include(r => r.Item)
                .Include(r => r.User)
                .Where(r => r.ItemId == itemId)  // Filter reviews for specific item
                .ToListAsync();

            if (!reviews.Any())
            {
                return Ok(new { AverageGrade = 0, Reviews = new List<ReviewDto>() });
            }

            var averageGrade = reviews.Average(r => r.Grade);
            var reviewDtos = mapper.Map<List<ReviewDto>>(reviews);

            return Ok(new
            {
                AverageGrade = averageGrade,
                Reviews = reviewDtos
            });
        }


        // GET: api/Review/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await dbContext.Reviews
                .Include(r => r.Item)
                .Include(r => r.User)  // Ensure user info is included
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            // Map Review and User to DTO
            var reviewView = mapper.Map<ReviewView>(review);

            // Manually map user details to UserView
            reviewView.User = new UserView
            {
               // Id = review.User.Id,
                Username = review.User.UserName,
                Email = review.User.Email,
                PhoneNumber = review.User.PhoneNumber
            };

            return Ok(reviewView);
        }


        // PUT: api/Review/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewDto reviewDto)
        {
            var review = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            review.Comment = reviewDto.Comment ?? review.Comment;
            review.Grade = reviewDto.Grade;

            await dbContext.SaveChangesAsync();
            return Ok("Review updated successfully.");
        }

        // DELETE: api/Review/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var review = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();
            return Ok("Review deleted successfully.");
        }




    }
}
