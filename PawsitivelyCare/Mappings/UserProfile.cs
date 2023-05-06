using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DTOs.User;

namespace PawsitivelyCare.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<CreateUserDto, UserModel>();
        }
    }
}
