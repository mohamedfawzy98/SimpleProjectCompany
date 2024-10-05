using DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Project_PL_.Helper
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			// smtp to Gmail

			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			// Gmail Sender
			client.Credentials = new NetworkCredential("mohamedfawzy9819@gmail.com", "jthaspbawleorotr");

			client.Send("mohamedfawzy9819@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}
