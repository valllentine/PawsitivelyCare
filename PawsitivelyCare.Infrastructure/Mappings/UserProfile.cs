using AutoMapper;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
            //CreateMap<UserDto, UserModel>();
        }
    }
}
