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
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        

        public PostController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPosts() {
            try
            {
                var posts = await _dbContext.Posts.Include(p => p.Categories).Include(p => p.Comments).Include(p => p.Tags).ToListAsync();
                return Ok(_mapper.Map<List<PostDto>>(posts));
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
                var post = await _dbContext.Posts.Include(p => p.Categories).Include(p => p.Comments).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

                if (post == null) return NotFound();

                return Ok(_mapper.Map<PostDto>(post));
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
                var newPost = _mapper.Map<Post>(requestPostDto);

                await _dbContext.Posts.AddAsync(newPost);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return Ok(_mapper.Map<PostDto>(newPost));
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
        public async Task<ActionResult<PostDto>> UpdatePost(RequestPostDto requestPostDto)
        {
            try
            {
                //var post = await _dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postRequest.Id);

                var post = await _dbContext.Posts.Where(p => p.Id == requestPostDto.Id).Include(p => p.Comments).Include(p=> p.Tags).Include(p => p.Categories).SingleOrDefaultAsync();

                if (post == null) return NotFound();

                var response = _mapper.Map(requestPostDto, post);

                _dbContext.SaveChanges();

                return _mapper.Map<PostDto>(response);
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
