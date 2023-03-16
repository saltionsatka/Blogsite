using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;

namespace Blogsite.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto> GetCategoryByIdAsync(int id);

        Task<CategoryDto?> CreateCategoryAsync(RequestCategoryDto requestCategoryDto);

        Task<CategoryDto?> UpdateCategoryAsync(RequestCategoryDto requestCategoryDto);

        Task<bool> DeleteCategoryAsync(int id);

    }
}
