using AutoMapper;
using Blogsite.Data;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
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
        private readonly ICommentService _commentService;

        public CommentController(AppDbContext dbContext, IMapper mapper, ICommentService commentService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments()
        {
            try
            {
                return await _commentService.GetCommentsAsync();
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
                var comment = await _commentService.GetCommentByIdAsync(id);

                if (comment == null) return NotFound();

                return Ok(comment);
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
                var newComment = await _commentService.CreateCommentAsync(requestCommentDto);
                if (newComment == null) return BadRequest();

                return Ok(newComment);
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
                var comment = await _commentService.UpdateCommentAsync(requestCommentDto);
                
                if (comment == null) return NotFound();

                return Ok(comment);
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
                var isDeleted = await _commentService.DeleteCommentAsync(id);

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
