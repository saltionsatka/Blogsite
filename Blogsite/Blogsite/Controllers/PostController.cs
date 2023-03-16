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
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(AppDbContext dbContext, IMapper mapper, IPostService postService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPosts() {
            try
            {
                return await _postService.GetPostsAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving posts from the database");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PostDto>> GetPostsById(int id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);

                if (post == null) return NotFound();

                return Ok(post);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving post from the database");
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<ActionResult<PostDto>> AddPost(RequestPostDto requestPostDto)
        {
            try
            {
                var newPost = await _postService.CreatePostAsync(requestPostDto);
                if (newPost == null) return BadRequest();
                
                return Ok(newPost);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error adding post to the database");
            }
        }


        [HttpPut]
        [Route("UpdatePost")]
        public async Task<ActionResult<PostDto>> UpdatePost(RequestPostDto requestPostDto)
        {
            try
            {
                var post = await _postService.UpdatePostAsync(requestPostDto);

                if (post == null) return NotFound();

                return Ok(post);
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
                var isDeleted = await _postService.DeletePostAsync(id);

                if (!isDeleted) return NotFound();

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
