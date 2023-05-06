namespace PawsitivelyCare.DAL.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}
