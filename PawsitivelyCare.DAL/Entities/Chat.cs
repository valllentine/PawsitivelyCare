namespace PawsitivelyCare.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}
