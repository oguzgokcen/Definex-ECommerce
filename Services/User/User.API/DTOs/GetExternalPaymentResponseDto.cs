namespace Ordering.API.DTOs
{
	public record GetExternalPaymentResponseDto
	{
		public string PaymentUrl { get; init; }
		public string ConversationId { get; init; }
		public string token { get; init; }
	}
}
