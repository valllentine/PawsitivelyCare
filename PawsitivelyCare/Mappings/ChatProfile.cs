using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DTOs.Chat;

namespace PawsitivelyCare.Mappings
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatModel, Chat>().ReverseMap();
            CreateMap<CreateChatDto, ChatModel>();
        }
    }
}
