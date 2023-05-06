﻿namespace PawsitivelyCare.DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
