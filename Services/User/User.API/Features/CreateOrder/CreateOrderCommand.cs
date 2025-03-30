namespace Ordering.API.Features.CreateOrder
{
	public record CreateOrderCommand : ICommand<CreateOrderResponse>
	{
		public Guid UserId {get; init;}
		public decimal Price { get; init; }
		public string UserEmail { get; init; } =default!;
		public string UserName { get; init; } = default!;
		public string UserPhone { get; init; }
		public List<BasketItemDto> Items { get; init; }
	}
}
