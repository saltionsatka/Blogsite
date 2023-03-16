using AutoMapper;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.DTO_Models;
using Blogsite.Interfaces;
using Blogsite.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _genericRepository;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepository<Comment> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto?> CreateCommentAsync(RequestCommentDto requestCommentDto)
        {
            try
            {
                var newComment = _mapper.Map<Comment>(requestCommentDto);
                await _genericRepository.Add(newComment);
                if (await _genericRepository.SaveChanges())
                {
                    return _mapper.Map<CommentDto>(newComment);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            try
            {
                var comment = await _genericRepository.GetById(id);

                if (comment == null) return false;

                _genericRepository.Remove(comment);
                await _genericRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CommentDto>> GetCommentsAsync()
        {
            try
            {
                var comments = await _genericRepository.GetAllAsQueryable().Include(c => c.Post).ToListAsync();

                return _mapper.Map<List<CommentDto>>(comments);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommentDto> GetCommentByIdAsync(int id)
        {
            try
            {
                var comment = await _genericRepository.GetAllAsQueryable().Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);
                return _mapper.Map<CommentDto>(comment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommentDto?> UpdateCommentAsync(RequestCommentDto requestCommentDto)
        {
            try
            {
                var comment = await _genericRepository.GetAllAsQueryable().FirstOrDefaultAsync(c => c.Id == requestCommentDto.Id);

                if (comment == null) return null;

                _mapper.Map(requestCommentDto, comment);
                _genericRepository.Update(comment);
                await _genericRepository.SaveChanges();


                return _mapper.Map<CommentDto>(comment);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
