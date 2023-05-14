using AutoMapper;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;

namespace PawsitivelyCare.BLL.Services.Realizations
{
    public class ChatService : IChatService
    {
        protected readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IBaseRepository<ChatMessage, Guid> _chatMessageRepository;
        public ChatService(IChatRepository chatRepository, IBaseRepository<ChatMessage, Guid> chatMessageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<List<ChatModel>> GetChats(Guid currentUserId)
        {
            var chats = await _chatRepository.GetChats(currentUserId);

            return _mapper.Map<List<ChatModel>>(chats);
        }

        public async Task<ChatModel> GetChat(Guid id)
        {
            var User = await _chatRepository.GetAsync(id);

            return _mapper.Map<ChatModel>(User);
        }


        public async Task<ChatModel> CreateChat(ChatModel chatModel, Guid currentUserId)
        {
            var chatEntity = _mapper.Map<Chat>(chatModel);
            var chat = await _chatRepository.AddChat(chatEntity, chatModel.PostCreatorId, currentUserId);

            return _mapper.Map<ChatModel>(chat);
        }

        public async Task<ChatMessageModel> CreateMessage(ChatMessageModel chatMessage)
        {
            var messageEntity = _mapper.Map<ChatMessage>(chatMessage);
            messageEntity.CreatedAt = DateTime.Now;

            var message = await _chatMessageRepository.AddAsync(messageEntity);

            return _mapper.Map<ChatMessageModel>(message);
        }

        public async Task<List<ChatMessageModel>> GetMessages(Guid chatId)
        {
            var messages = await _chatMessageRepository.Query(m => m.ChatId == chatId);
            return _mapper.Map<List<ChatMessageModel>>(messages.OrderBy(m => m.CreatedAt));
        }
    }
}
