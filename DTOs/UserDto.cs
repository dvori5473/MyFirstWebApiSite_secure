using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs
{
    public class UserDto
    {
        [Required,EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "first name must be less than 20 characters long")]
        public string? FirstName { get; set; }
        [MaxLength(20, ErrorMessage= "last name must be less than 20 characters long")]
        public string? LastName { get; set; }
        [Required]
        public string Password { get; set; } = null!;

    }
}
