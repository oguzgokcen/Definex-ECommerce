namespace Ordering.API.Features.CreateOrder
{
	public class CreateOrderResponse
	{
		public bool IsSuccess {get; set;}
		public string PaymentPageUrl { get; set; }
	}
}
