namespace PawsitivelyCare.BLL.Models
{
    public class ChatMessageModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
    }
}
