using Blogsite.Data;
using Blogsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blogsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly AppDbContext _dbContext;
        public TagController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetTags()
        {
            try
            {
                var tags = await _dbContext.Tags.ToListAsync();
                return tags;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving tags from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Tag>> GetTagById(int id)
        {
            try
            {
                var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

                if (tag == null) return NotFound();

                return tag;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving tag from database");
            }
        }

        [HttpPost]
        [Route("AddTag")]
        public async Task<ActionResult<Tag>> AddTag(Tag tagRequest)
        {
            try
            {
                await _dbContext.Tags.AddAsync(tagRequest);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(tagRequest);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding tag to the database");
            }
        }

        [HttpPut]
        [Route("UpdateTag")]
        public async Task<ActionResult<Tag>> UpdateTag(Tag tagRequest)
        {
            try
            {
                var tag = await _dbContext.Tags.FirstOrDefaultAsync(c => c.Id == tagRequest.Id);
                if (tag == null) return NotFound();

                tag.Title = tagRequest.Title;
                tag.Slug = tagRequest.Slug;
                tag.Content = tagRequest.Content;

                _dbContext.SaveChanges();

                return tagRequest;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating tag from the database");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

                if (comment == null) return NotFound();

                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting comment from the database");
            }
        }
    }
}
