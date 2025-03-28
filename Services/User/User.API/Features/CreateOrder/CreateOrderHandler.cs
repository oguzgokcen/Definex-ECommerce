using Ordering.API.Repositories;
using Ordering.API.Data.Entities;

namespace Ordering.API.Features.CreateOrder
{
	public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
	{
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateOrderHandler(IGenericRepository<Order> orderRepository , IUnitOfWork unitOfWork)
		{
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
		{
			var order = new Order
			{
				UserId = command.UserId,
				Price = command.Price,
				Status = OrderStatus.Processing,
				OrderItems = command.Items.Select(item => new OrderItem
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity,
				}).ToList()
			};

			await _orderRepository.AddAsync(order, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return new CreateOrderResponse
			{
				IsSuccess = true,
				OrderId = order.Id
			};
		}
	}
}
