using AutoMapper;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Interfaces;
using Blogsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogsite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto?> CreateCategoryAsync(RequestCategoryDto requestCategoryDto)
        {
            try
            {
                var newCategory = _mapper.Map<Category>(requestCategoryDto);
                await _genericRepository.Add(newCategory);
                if (await _genericRepository.SaveChanges())
                {
                    return _mapper.Map<CategoryDto>(newCategory);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _genericRepository.GetById(id);

                if (category == null) return false;

                _genericRepository.Remove(category);
                await _genericRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _genericRepository.GetAll();
                return _mapper.Map<List<CategoryDto>>(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _genericRepository.GetAllAsQueryable().Include(c => c.Posts).FirstOrDefaultAsync(c=> c.Id == id);
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(RequestCategoryDto requestCategoryDto)
        {
            try
            {
                var category = await _genericRepository.GetAllAsQueryable().FirstOrDefaultAsync(c => c.Id == requestCategoryDto.Id);

                if(category == null) return null;

                _mapper.Map(requestCategoryDto, category);
                _genericRepository.Update(category);
                await _genericRepository.SaveChanges();


                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
