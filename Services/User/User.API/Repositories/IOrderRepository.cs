namespace Ordering.API.Repositories
{
	public interface IOrderRepository : IGenericRepository<Order>
	{
		Task<Order> GetByPaymentToken(string paymentToken);
	}
}
