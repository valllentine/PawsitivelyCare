namespace PawsitivelyCare.DTOs.Chat
{
    public class CreateChatMessageDto
    {
        public string Text { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
    }
}
