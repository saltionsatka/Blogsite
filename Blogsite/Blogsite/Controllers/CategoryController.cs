using AutoMapper;
using Blogsite.Data;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
using Blogsite.Models;
using Blogsite.Services;
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
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext dbContext, IMapper mapper, ICategoryService categoryService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            try
            {
                return await _categoryService.GetCategoriesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving categories from database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                
                if (category == null) return NotFound();

                return Ok(category);
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
                var newCategory = await _categoryService.CreateCategoryAsync(requestCategoryDto);
                if (newCategory == null) return BadRequest();
                    
                return Ok(newCategory);
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
                var category = await _categoryService.UpdateCategoryAsync(requestCategoryDto);

                if (category == null) return NotFound();

                return Ok(category);
            }
            catch(Exception ex)
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
                var isDeleted = await _categoryService.DeleteCategoryAsync(id);

                if(!isDeleted) return NotFound();

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
