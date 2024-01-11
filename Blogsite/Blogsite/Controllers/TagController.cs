using AutoMapper;
using Blogsite.Data;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
using Blogsite.Models;
using Blogsite.Services;
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
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;

        public TagController(AppDbContext dbContext, IMapper mapper, ITagService tagService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetTags()
        {
            try
            {
                return await _tagService.GetTagsAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving tags from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TagDto>> GetTagById(int id)
        {
            try
            {
                var tag = await _tagService.GetTagByIdAsync(id);

                if (tag == null) return NotFound();

                return Ok(tag);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving tag from database");
            }
        }

        [HttpPost]
        [Route("AddTag")]
        public async Task<ActionResult<TagDto>> AddTag(RequestTagDto requestTagDto)
        {
            try
            {
                var newTag = await _tagService.CreateTagAsync(requestTagDto);
                if (newTag == null) return BadRequest();

                return Ok(newTag);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding tag to the database");
            }
        }

        [HttpPut]
        [Route("UpdateTag")]
        public async Task<ActionResult<TagDto>> UpdateTag(RequestTagDto requestTagDto)
        {
            try
            {
                var tag = await _tagService.UpdateTagAsync(requestTagDto);
                
                if (tag == null) return NotFound();

                return Ok(tag);
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
                var isDeleted = await _tagService.DeleteTagAsync(id);

                if (!isDeleted) return NotFound();

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
