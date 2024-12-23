using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class ForgetPasswordVM
    {

        [EmailAddress(ErrorMessage = "invalid email")]
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

    }
}
