namespace Ordering.API.Features.CreateOrder
{
	public record CreateOrderCommand : ICommand<CreateOrderResponse>
	{
		public Guid UserId {get; init;}
		public decimal Price { get; init; }
		public List<BasketItemDto> Items { get; init; }
	}
}
