using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
