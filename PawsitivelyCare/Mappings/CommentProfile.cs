using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentUserModel>().ReverseMap();
        }
    }
}
