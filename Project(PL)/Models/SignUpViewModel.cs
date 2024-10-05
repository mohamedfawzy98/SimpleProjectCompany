using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "FName is Required")]
        public string FName { get; set; }
        [Required(ErrorMessage = "LName is Required")]
        public string LName { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Not ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

    }
}
