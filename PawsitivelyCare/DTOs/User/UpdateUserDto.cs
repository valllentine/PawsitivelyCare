﻿namespace PawsitivelyCare.DTOs.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Password { get; set; }
    }
}
