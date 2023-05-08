﻿using System.ComponentModel.DataAnnotations;

namespace PawsitivelyCare.DTOs.User
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
