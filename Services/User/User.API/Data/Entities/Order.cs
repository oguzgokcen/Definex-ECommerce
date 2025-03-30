namespace Ordering.API.Data.Entities
{
	public class Order : BaseEntity
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public decimal Price { get; set; }
		public OrderStatus Status { get; set; }
		public string? PaymentToken { get; set; }
		public List<OrderItem> OrderItems{ get; set; }
	}
}
