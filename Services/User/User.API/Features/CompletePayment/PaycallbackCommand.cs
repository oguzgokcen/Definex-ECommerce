namespace Ordering.API.Features.CompletePayment;
public class PayCallbackCommand : ICommand<PayCallbackResponse>
{
	public string token { get; set; }
}

