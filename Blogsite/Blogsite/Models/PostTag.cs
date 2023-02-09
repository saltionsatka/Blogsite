namespace Blogsite.Models
{
    public class PostTag
    {
        public int PostsId { get; set; }
        public virtual Post? Post { get; set; }

        public int TagsId { get; set; }
        public virtual Tag? Tag { get; set; }
    }
}
