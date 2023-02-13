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
    public class CommentController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CommentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> GetComments()
        {
            try
            {
                var comments = await _dbContext.Comments.ToListAsync();
                return comments;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving comments from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

                if (comment == null) return NotFound();

                return comment;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving comment from database");
            }
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<ActionResult<Comment>> AddComment(Comment commentRequest)
        {
            try
            {
                await _dbContext.Comments.AddAsync(commentRequest);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(commentRequest);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding comment to the database");
            }
        }

        [HttpPut]
        [Route("UpdateComment")]
        public async Task<ActionResult<Comment>> UpdateComment(Comment commentRequest)
        {
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentRequest.Id);
                if (comment == null) return NotFound();

                comment.Title = commentRequest.Title;
                comment.Published = true;
                comment.Content = commentRequest.Content;

                _dbContext.SaveChanges();

                return commentRequest;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating comment from the database");
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
