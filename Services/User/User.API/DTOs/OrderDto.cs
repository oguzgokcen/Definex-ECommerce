namespace Ordering.API.DTOs
{
	public class OrderDto
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public DateTime CreatedOnUtc { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public List<BasketItemDto> BasketItems { get; set; }
	}
}
