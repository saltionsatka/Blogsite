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
    public class CommentController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments()
        {
            try
            {
                var comments = await _dbContext.Comments.Include(c => c.Post).ToListAsync();
                return Ok(_mapper.Map<List<CommentDto>>(comments));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving comments from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            try
            {
                var comment = await _dbContext.Comments.Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

                if (comment == null) return NotFound();

                return Ok(_mapper.Map<CommentDto>(comment));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving comment from database");
            }
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<ActionResult<CommentDto>> AddComment(RequestCommentDto requestCommentDto)
        {
            try
            {
                var newComment = _mapper.Map<Comment>(requestCommentDto);

                await _dbContext.Comments.AddAsync(newComment);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(_mapper.Map<CommentDto>(newComment));
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
        public async Task<ActionResult<CommentDto>> UpdateComment(RequestCommentDto requestCommentDto)
        {
            try
            {
                var comment = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == requestCommentDto.Id);
                
                if (comment == null) return NotFound();

                var response = _mapper.Map(requestCommentDto, comment);

                _dbContext.SaveChanges();

                return _mapper.Map<CommentDto>(response);
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
