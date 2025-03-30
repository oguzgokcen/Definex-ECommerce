namespace Ordering.API.Features.CompletePayment
{
	public class PayCallbackResponse
	{
		public int OrderId { get; set; }
		public bool IsSuccess { get; set; }
	}
}
