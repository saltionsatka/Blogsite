using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;

namespace Blogsite.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetCommentsAsync();

        Task<CommentDto> GetCommentByIdAsync(int id);

        Task<CommentDto?> CreateCommentAsync(RequestCommentDto requestCommentDto);

        Task<CommentDto?> UpdateCommentAsync(RequestCommentDto requestCommentDto);

        Task<bool> DeleteCommentAsync(int id);
    }
}
