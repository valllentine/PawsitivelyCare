using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DTOs.Post;

namespace PawsitivelyCare.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostModel, Post>().ReverseMap();
            CreateMap<CreatePostDto, PostModel>();
            CreateMap<UpdatePostDto, PostModel>();
        }
    }
}
