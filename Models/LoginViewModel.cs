using System.ComponentModel.DataAnnotations;

namespace Project01.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
