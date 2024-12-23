using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class LoginVM
    {

        [EmailAddress(ErrorMessage = "invalid email")]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "min len 6")]
        [Required(ErrorMessage = "password required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
