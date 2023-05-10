namespace PawsitivelyCare.DAL.Entities
{
    public class User
    {
        public enum UserGender
        { 
            Other = 0,
            Female = 1,
            Male = 2,
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserGender Gender { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
