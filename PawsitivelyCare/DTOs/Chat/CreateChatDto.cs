namespace PawsitivelyCare.DTOs.Chat
{
    public class CreateChatDto
    {
        public string Name { get; set; }
        public Guid PostCreatorId { get; set; }
        public Guid PostId { get; set; }
    }
}
