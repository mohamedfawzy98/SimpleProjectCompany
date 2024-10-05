using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
	public class RestPasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]	
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
