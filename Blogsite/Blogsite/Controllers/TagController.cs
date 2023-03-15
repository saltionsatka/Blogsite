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
using Microsoft.Extensions.Hosting;

namespace Blogsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TagController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetTags()
        {
            try
            {
                var tags = await _dbContext.Tags.ToListAsync();
                return Ok(_mapper.Map<List<TagDto>>(tags));
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
                var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

                if (tag == null) return NotFound();

                return Ok(_mapper.Map<TagDto>(tag));
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
                var newTag = _mapper.Map<Tag>(requestTagDto);

                await _dbContext.Tags.AddAsync(newTag);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(_mapper.Map<TagDto>(newTag));
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
        public async Task<ActionResult<TagDto>> UpdateTag(RequestTagDto requestTagDto)
        {
            try
            {
                var tag = await _dbContext.Tags.FirstOrDefaultAsync(c => c.Id == requestTagDto.Id);
                if (tag == null) return NotFound();

                var response = _mapper.Map(requestTagDto, tag);

                _dbContext.SaveChanges();

                return _mapper.Map<TagDto>(response);
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
