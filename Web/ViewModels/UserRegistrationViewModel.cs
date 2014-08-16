using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Web.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email address: ")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password: ")]
        public string ConfirmPassword { get; set; }
    }
}