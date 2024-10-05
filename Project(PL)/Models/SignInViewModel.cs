using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool  RemmemberMe { get; set; }
	}
}
