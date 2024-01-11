using AutoMapper;
using Blogsite.DTO_Models;
using Blogsite.DTO_Models.RequestDtos;
using Blogsite.Models;

namespace Blogsite.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Tag, TagDto>();


            CreateMap<RequestPostDto, Post>();
            CreateMap<RequestCommentDto, Comment>();
            CreateMap<RequestCategoryDto, Category>();
            CreateMap<RequestTagDto, Tag>();
        }
    }
}
