namespace PawsitivelyCare.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
