﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondProject.context;
using secondProject.Dtos;
using secondProject.Models;

namespace secondProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> Get()
        {
            try
            {
                var review = await _context.Reviews.ToListAsync();
                var reviewDto = review.Select(review => new GetReviewDTO
                {
                   Id = review.Id,
                   Rating = review.Rating,
                   Comment = review.Comment,

                });
                return Ok(reviewDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error Occured. {e.Message}");
            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Review>>> Create([FromBody] ReviewDto reviewDto)
        {
            try
            {
                var review = new Review
                {
                    Rating = reviewDto.Rating,
                    Comment = reviewDto.Comment,
                    HotelId = reviewDto.HotelId,
                    userId = reviewDto.UserId
                };
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return Ok("Added review Successfull");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Review>>> Delete(int id)
        {
            try
            {
                var findReview = await _context.Reviews.FindAsync(id);
                if (findReview == null)
                {
                    return NotFound();
                }
                _context.Reviews.Remove(findReview);
                await _context.SaveChangesAsync();
                return Ok("Review Deleted Successfully");
            }
            catch (Exception e)
            {
                return BadRequest($"Invalid argument: {e.Message}");

            }

        }

    }
}
