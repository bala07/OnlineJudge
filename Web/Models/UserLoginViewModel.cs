using System.ComponentModel.DataAnnotations;

namespace OnlineJudge.Web.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email address: ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

    }
}