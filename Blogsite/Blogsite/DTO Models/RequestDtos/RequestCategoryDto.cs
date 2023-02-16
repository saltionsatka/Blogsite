namespace Blogsite.DTO_Models.RequestDtos
{
    public class RequestCategoryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Content { get; set; }

        public virtual ICollection<PostDto>? Posts { get; set; }
    }
}
