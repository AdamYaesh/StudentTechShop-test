using System.ComponentModel.DataAnnotations;

namespace StudentTechShop.API.Models.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }

      //  public string? IBAN { get; set; }

        //  public string[]  Roles { get; set; }
    }
}
