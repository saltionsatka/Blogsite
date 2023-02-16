using AutoMapper;
using Blogsite.Data;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
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
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await _dbContext.Categories.Include(c => c.Posts).ToListAsync();
                return Ok(_mapper.Map<List<CategoryDto>>(categories));
            } 
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving categories from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await _dbContext.Categories.Include(c => c.Posts).FirstOrDefaultAsync(c => c.Id == id);

                if (category == null) return NotFound();

                return Ok(_mapper.Map<CategoryDto>(category));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving category from database");
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult<CategoryDto>> AddCategory(RequestCategoryDto requestCategoryDto)
        {
            try
            {
                var newCategory = _mapper.Map<Category>(requestCategoryDto);
                await _dbContext.Categories.AddAsync(newCategory);
                if(await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(_mapper.Map<CategoryDto>(newCategory));
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
        public async Task<ActionResult<CategoryDto>> UpdateCategory(RequestCategoryDto requestCategoryDto)
        {
            try
            {
                var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == requestCategoryDto.Id);
                
                if (category == null) return NotFound();

                var response = _mapper.Map(requestCategoryDto, category);

                _dbContext.SaveChanges();

                return _mapper.Map<CategoryDto>(response);
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
