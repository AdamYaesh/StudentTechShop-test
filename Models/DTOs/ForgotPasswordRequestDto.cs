using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequestDto
    {
        public string Email { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The Password and cinfirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
