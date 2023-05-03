namespace PawsitivelyCare.BLL.Models
{
    public class ChatMessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
    }
}
