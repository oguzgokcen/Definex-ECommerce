namespace User.API.Exceptions
{
	public class AuthFailedException : ValidationException
	{
		public AuthFailedException() : base("Email veya şifre yanlış!")
		{
		}
	}
}
