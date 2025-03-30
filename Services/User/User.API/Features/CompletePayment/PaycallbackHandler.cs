

using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using Ordering.API.Exceptions;

namespace Ordering.API.Features.CompletePayment
{
	public class PayCallbackCommandHandler : ICommandHandler<PayCallbackCommand, PayCallbackResponse>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IyzicoOptions _iyzicoOptions;

		public PayCallbackCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOptions<IyzicoOptions> options)
		{
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
			_iyzicoOptions = options.Value;
		}
		public async Task<PayCallbackResponse> Handle(PayCallbackCommand request, CancellationToken cancellationToken)
		{
			var order = await _orderRepository.GetByPaymentToken(request.token);
			if (order == null)
			{
				throw new OrderNotFoundException();
			}
			RetrieveCheckoutFormRequest cfRetrieveRequest = new RetrieveCheckoutFormRequest();
			cfRetrieveRequest.Token = request.token;
			Iyzipay.Options options = new()
			{
				ApiKey = _iyzicoOptions.ApiKey,
				SecretKey = _iyzicoOptions.SecretKey,
				BaseUrl = _iyzicoOptions.BaseUrl,
			};
			CheckoutForm response = await CheckoutForm.Retrieve(cfRetrieveRequest, options);
			if (response == null)
			{
				throw new PaymentFailException("Ödeme sağlayıcı cevapsız döndü.");
			}
			else
			{
				if(response.StatusCode != 200)
				{
					order.Status = OrderStatus.PaymentFailed;
				}
				else
				{
					order.Status = OrderStatus.Processing;
				}
			}
			_orderRepository.Update(order);
			await _unitOfWork.SaveChangesAsync();
			return new PayCallbackResponse { OrderId = order.Id, IsSuccess = true };
		}
	}
}
