using AutoMapper;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
using Blogsite.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Services
{
    public class TagService : ITagService
    {
        private readonly IGenericRepository<Tag> _genericRepository;
        private readonly IMapper _mapper;

        public TagService(IGenericRepository<Tag> genericRepository,  IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<TagDto?> CreateTagAsync(RequestTagDto requestTagDto)
        {
            try
            {
                var newTag = _mapper.Map<Tag>(requestTagDto);
                await _genericRepository.Add(newTag);
                if (await _genericRepository.SaveChanges())
                {
                    return _mapper.Map<TagDto>(newTag);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            try
            {
                var tag = await _genericRepository.GetById(id);

                if (tag == null) return false;

                _genericRepository.Remove(tag);
                await _genericRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TagDto> GetTagByIdAsync(int id)
        {
            try
            {
                var tag = await _genericRepository.GetById(id);

                return _mapper.Map<TagDto>(tag);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TagDto>> GetTagsAsync()
        {
            try
            {
                var tags = await _genericRepository.GetAll();
                return _mapper.Map<List<TagDto>>(tags);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TagDto?> UpdateTagAsync(RequestTagDto requestTagDto)
        {
            try
            {
                var tag = await _genericRepository.GetAllAsQueryable().FirstOrDefaultAsync(t => t.Id == requestTagDto.Id);

                if (tag == null) return null;

                _mapper.Map(requestTagDto, tag);
                _genericRepository.Update(tag);
                await _genericRepository.SaveChanges();


                return _mapper.Map<TagDto>(tag);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
