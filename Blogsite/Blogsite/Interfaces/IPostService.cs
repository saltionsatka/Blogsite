using Blogsite.DTO_Models.RequestDtos;
using Blogsite.DTO_Models;

namespace Blogsite.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDto>> GetPostsAsync();

        Task<PostDto> GetPostByIdAsync(int id);

        Task<PostDto?> CreatePostAsync(RequestPostDto requestPostDto);

        Task<PostDto?> UpdatePostAsync(RequestPostDto requestPostDto);

        Task<bool> DeletePostAsync(int id);
    }
}
