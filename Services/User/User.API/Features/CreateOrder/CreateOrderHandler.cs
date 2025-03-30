using Ordering.API.Repositories;
using Ordering.API.Services;

namespace Ordering.API.Features.CreateOrder
{
	public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
	{
		private readonly IGenericRepository<Order> _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPaymentService _paymentService;

		public CreateOrderHandler(IGenericRepository<Order> orderRepository , IUnitOfWork unitOfWork, IPaymentService paymentService)
		{
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
			_paymentService = paymentService;
		}

		public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
		{
			var order = new Order
			{
				UserId = command.UserId,
				Price = command.Price,
				Status = OrderStatus.PrePayment,
				OrderItems = command.Items.Select(item => new OrderItem
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity,
				}).ToList()
			};

			var paymentResponse = await _paymentService.Pay(new CreatePaymentDto(order.Id.ToString(),command.UserId.ToString(),command.UserName,command.UserEmail,command.UserPhone,order.Id.ToString(),order.Id.ToString(),order.Price),cancellationToken);

			order.PaymentToken = paymentResponse.token;
			await _orderRepository.AddAsync(order, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return new CreateOrderResponse
			{
				IsSuccess = true,
				PaymentPageUrl = paymentResponse.PaymentUrl,
			};
		}
	}
}
