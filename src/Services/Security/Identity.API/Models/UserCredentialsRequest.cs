namespace Identity.API.Models
{
	public class UserCredentialsRequest
	{
		public string UserName { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;
	}
}
