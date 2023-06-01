using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DTOs.Comment;
using PawsitivelyCare.DTOs.Post;

namespace PawsitivelyCare.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentUserModel>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<User, CommentUserModel>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src))
                .ReverseMap();

            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<CreateCommentDto, CommentModel>();
        }
    }
}
