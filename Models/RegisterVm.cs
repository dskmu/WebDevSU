using System.ComponentModel.DataAnnotations;

namespace WEBKA.Models
{
    public class RegisterVm
    {


        [Required, Display(Name = "Name"), StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required, Display(Name = "LastName"), StringLength(100)]
        public string LastName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = "";

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = "";



    }
}
