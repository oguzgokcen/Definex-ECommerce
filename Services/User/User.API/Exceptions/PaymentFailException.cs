namespace Ordering.API.Exceptions
{
	public class PaymentFailException:Exception
	{
		public PaymentFailException(string errorMessage):base($"Ödeme başarısız: {errorMessage}") { }
	}
}
