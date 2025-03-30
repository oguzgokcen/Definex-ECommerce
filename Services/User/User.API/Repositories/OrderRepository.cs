using Microsoft.EntityFrameworkCore;
using Ordering.API.Data;

namespace Ordering.API.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
         private readonly DbSet<Order> _dbSet;
		public OrderRepository(OrderDbContext context) : base(context)
		{
			_dbSet = context.Set<Order>();
		}

		public async Task<Order> GetByPaymentToken(string paymentToken)
		{
			var order = await _dbSet.Where(x => x.PaymentToken == paymentToken).FirstOrDefaultAsync();
			return order;
		}

	}
}
