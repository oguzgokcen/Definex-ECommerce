namespace Ordering.API.DTOs
{
	public record CreatePaymentDto(
		string OrderId,
		string UserId,
		string UserName,
		string UserEmail,
		string UserPhone,
		string ProductId,
		string ProductName,
		decimal OrderPrice
		);
}
