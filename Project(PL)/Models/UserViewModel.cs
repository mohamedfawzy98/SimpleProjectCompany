namespace Project_PL_.Models
{
	public class UserViewModel
	{
		public string FName { get; set; }
		public string Id { get; set; }
		public string LName { get; set; }
		public string Email { get; set; }
		public IEnumerable<string>? Roles { get; set; }
	}
}
