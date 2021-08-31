using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "{0} field is required")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} field is required")]
        [StringLength(100, ErrorMessage = "{0} field must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match.")]
        public string PasswordConfirm { get; set; }
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "{0} field is required")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} field is required")]
        [StringLength(100, ErrorMessage = "{0} field must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
