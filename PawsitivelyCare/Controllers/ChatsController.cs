using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsitivelyCare.BLL.Models;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.DTOs.Chat;
using System.Security.Claims;

namespace PawsitivelyCare.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/chats/")]
    public class ChatsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public ChatsController(IMapper mapper, IChatService chatService, IUserService userService)
        {
            _mapper = mapper;
            _chatService = chatService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetChats()
        {
            var chats = await _chatService.GetChats(UserId);

            return Ok(chats);
        }

        [HttpGet("chatInfo")]
        public async Task<ActionResult> GetChat([FromQuery] Guid chatId)
        {
            var chat = await _chatService.GetChat(chatId);
            return Ok(chat);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChat(CreateChatDto createChatDto)
        {
            var chatModel = _mapper.Map<ChatModel>(createChatDto);

            var createdChat = await _chatService.CreateChat(chatModel, UserId);
            return Ok(createdChat);
        }

        [HttpPost, Route("messages")]
        public async Task<ActionResult> CreateMessage(CreateChatMessageDto createMessageDto)
        {
            var createMessageModel = _mapper.Map<ChatMessageModel>(createMessageDto);

            var id = await _chatService.CreateMessage(createMessageModel);
            return Ok(id);
        }

        [HttpGet, Route("messages")]
        public async Task<ActionResult> GetMessages([FromQuery] Guid chatId)
        {
            var messages = await _chatService.GetMessages(chatId);
            return Ok(messages);
        }
    }
}
