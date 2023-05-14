
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.DAL.Repositories.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat, Guid>
    {
        Task<Chat> AddChat(Chat chat, Guid postCreatorId, Guid currentUserId);
        Task<List<Chat>> GetChats(Guid currentUserId);
    }
}
