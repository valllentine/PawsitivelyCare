using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PawsitivelyCare.DAL.Contexts;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.DAL.Repositories.Realizations
{
    public class ChatRepository : BaseRepository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(IDbContextFactory<PawsitivelyCareDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<List<Chat>> GetChats(Guid currentUserId)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Chats
                .Include(c => c.Users)
                .Where(c => c.Users.Any(u => u.Id == currentUserId))
                .ToListAsync();
        }

        public async Task<Chat> AddChat(Chat chat, Guid postCreatorId, Guid currentUserId)
        {
            using var context = _contextFactory.CreateDbContext();

            var existingChat = await context.Chats
                .Include(c => c.Users)
                .Where(c => c.Users.Any(u => u.Id == postCreatorId))
                .FirstOrDefaultAsync(c => c.Users.Any(u => u.Id == currentUserId));

            if (existingChat != null)
            {
                return existingChat;
            }

            var existingUser1 = await context.Users.FindAsync(postCreatorId);
            var existingUser2 = await context.Users.FindAsync(currentUserId);

            chat.Users = new List<User> { existingUser1, existingUser2 };

            context.Chats.Add(chat);
            await context.SaveChangesAsync();

            return chat;
        }

    }
}
