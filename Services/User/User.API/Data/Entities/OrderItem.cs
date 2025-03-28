namespace Ordering.API.Data.Entities;

public class OrderItem : BaseEntity
{
	public int Id { get; set; }
	public long ProductId { get; set; }
	public int Quantity { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
}

