using Blogsite.DTO_Models.RequestDtos;
using Blogsite.DTO_Models;

namespace Blogsite.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetTagsAsync();

        Task<TagDto> GetTagByIdAsync(int id);

        Task<TagDto?> CreateTagAsync(RequestTagDto requestTagDto);

        Task<TagDto?> UpdateTagAsync(RequestTagDto requestTagDto);

        Task<bool> DeleteTagAsync(int id);
    }
}
