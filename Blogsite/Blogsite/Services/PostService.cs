using AutoMapper;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
using Blogsite.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _genericRepository;
        private readonly IMapper _mapper;

        public PostService(IGenericRepository<Post> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }
        public async Task<PostDto?> CreatePostAsync(RequestPostDto requestPostDto)
        {
            try
            {
                var newPost = _mapper.Map<Post>(requestPostDto);
                await _genericRepository.Add(newPost);
                if (await _genericRepository.SaveChanges())
                {
                    return _mapper.Map<PostDto>(newPost);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            try
            {
                var post = await _genericRepository.GetById(id);

                if (post == null) return false;

                _genericRepository.Remove(post);
                await _genericRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            try
            {
                var post = await _genericRepository.GetAllAsQueryable().Include(p => p.Categories).Include(p => p.Comments).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

                return _mapper.Map<PostDto>(post);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PostDto>> GetPostsAsync()
        {
            try
            {
                var posts = await _genericRepository.GetAllAsQueryable().Include(p => p.Categories).Include(p => p.Comments).Include(p => p.Tags).ToListAsync();
                return _mapper.Map<List<PostDto>>(posts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PostDto?> UpdatePostAsync(RequestPostDto requestPostDto)
        {
            try
            {
                var post = await _genericRepository.GetAllAsQueryable().FirstOrDefaultAsync(p => p.Id == requestPostDto.Id);

                if (post == null) return null;

                _mapper.Map(requestPostDto, post);
                _genericRepository.Update(post);
                await _genericRepository.SaveChanges();


                return _mapper.Map<PostDto>(post);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
