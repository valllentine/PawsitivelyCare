using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DTOs.Chat;

namespace PawsitivelyCare.Mappings
{
    public class ChatMessageProfile : Profile
    {
        public ChatMessageProfile()
        {
            CreateMap<ChatMessageModel, ChatMessage>().ReverseMap();
            CreateMap<CreateChatMessageDto, ChatMessageModel>();
        }
    }
}
