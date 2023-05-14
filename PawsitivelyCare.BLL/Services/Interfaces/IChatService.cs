using PawsitivelyCare.BLL.Models;

namespace PawsitivelyCare.BLL.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatModel>> GetChats(Guid currentUserId);
        Task<ChatModel> GetChat(Guid id);
        Task<ChatModel> CreateChat(ChatModel chatModel, Guid currentUserId);
        Task<ChatMessageModel> CreateMessage(ChatMessageModel chatMessage);
        Task<List<ChatMessageModel>> GetMessages(Guid id);
    }
}
