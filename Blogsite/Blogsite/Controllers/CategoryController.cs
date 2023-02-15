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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            try
            {
                var categories = await _dbContext.Categories.Include(c => c.Posts).ToListAsync();
                return categories;
            } 
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving categories from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _dbContext.Categories.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);

                if (category == null) return NotFound();

                return category;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving category from database");
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult<Category>> AddCategory(Category categoryRequest)
        {
            try
            {
                await _dbContext.Categories.AddAsync(categoryRequest);
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(categoryRequest);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding category to the database");
            }
        }

        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(Category categoryRequest)
        {
            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryRequest.Id);
                if (category == null) return NotFound();

                category.Title = categoryRequest.Title;
                category.MetaTitle = categoryRequest.MetaTitle;
                category.Slug = categoryRequest.Slug;
                category.Content = categoryRequest.Content;

                _dbContext.SaveChanges();

                return category;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating category from the database");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if(category == null) return NotFound();

                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting category from the database");
            }
        }
    }
}
