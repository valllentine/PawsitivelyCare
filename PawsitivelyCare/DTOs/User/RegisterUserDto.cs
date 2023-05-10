using System.ComponentModel.DataAnnotations;
using static PawsitivelyCare.DAL.Entities.User;

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
        //[Phone]
        public string Phone { get; set; }

        [Required]
        public UserGender Gender { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
