using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Pkcs;

namespace ST10355869_PROG6212_Part2.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        
    }
}
