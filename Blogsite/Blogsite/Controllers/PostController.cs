using Blogsite.Data;
using Blogsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PostController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts() {
            try
            {
                var posts = await _dbContext.Posts.ToListAsync();
                return posts;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving posts from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Post>> GetPostsById(int id)
        {
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);

                if (post == null) return NotFound();

                return post;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving post from the database");
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<ActionResult<Post>> AddPost(Post postRequest)
        {
            try
            {
                await _dbContext.Posts.AddAsync(postRequest);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(postRequest);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding post to the database");
            }
        }


        [HttpPut]
        [Route("UpdatePost")]
        public async Task<ActionResult<Post>> UpdatePost(Post postRequest)
        {
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postRequest.Id);

                if (post == null) return NotFound();

                post.Title= postRequest.Title;
                post.MetaTitle = postRequest.MetaTitle;
                post.Slug = postRequest.Slug;
                post.Summary = postRequest.Summary;
                post.Published = true;
                post.UpdatedAt = DateTime.Now;
                post.Content = postRequest.Content;

                _dbContext.SaveChanges();

                return post;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating post from the database");
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);

                if (post == null) return NotFound();

                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting post from the database");
            }
        }


    }
}
