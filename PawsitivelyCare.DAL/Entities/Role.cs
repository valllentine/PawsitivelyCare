﻿namespace PawsitivelyCare.DAL.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
