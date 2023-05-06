namespace PawsitivelyCare.DAL.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual User Sender { get; set; }
    }
}
