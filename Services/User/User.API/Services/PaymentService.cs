using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using Ordering.API.Exceptions;
using System.Globalization;

namespace Ordering.API.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly IyzicoOptions _iyzicoOptions;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public PaymentService(IHttpContextAccessor httpContextAccessor, IOptions<IyzicoOptions> iyzicoOptions)
		{
			_iyzicoOptions = iyzicoOptions.Value;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<GetExternalPaymentResponseDto> Pay(CreatePaymentDto paymentDto, CancellationToken cancellationToken)
		{
			Iyzipay.Options options = new()
			{
				ApiKey = _iyzicoOptions.ApiKey,
				SecretKey = _iyzicoOptions.SecretKey,
				BaseUrl = _iyzicoOptions.BaseUrl,
			};

			var httpRequest = _httpContextAccessor.HttpContext?.Request;
			var baseUrl = $"https://localhost:5054";

			CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
			request.Locale = Locale.TR.ToString();
			request.ConversationId = paymentDto.OrderId;
			request.Price = paymentDto.OrderPrice.ToString(CultureInfo.InvariantCulture);
			request.PaidPrice = paymentDto.OrderPrice.ToString(CultureInfo.InvariantCulture);
			request.Currency = Currency.TRY.ToString();
			request.BasketId = Guid.NewGuid().ToString();
			request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
			request.CallbackUrl = $"{baseUrl}/api/order/paycallback";

			Buyer buyer = new Buyer();
			buyer.Id = paymentDto.UserId;
			buyer.Name = paymentDto.UserName;
			buyer.Surname = paymentDto.UserName;
			buyer.GsmNumber = paymentDto.UserPhone;
			buyer.Email = paymentDto.UserEmail;
			buyer.IdentityNumber = "74300864791";
			buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			buyer.City = "Istanbul";
			buyer.Country = "Turkey";
			buyer.ZipCode = "34732";
			request.Buyer = buyer;

			Address billingAddress = new Address();
			billingAddress.ContactName = "Jane Doe";
			billingAddress.City = "Istanbul";
			billingAddress.Country = "Turkey";
			billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			billingAddress.ZipCode = "34742";
			request.BillingAddress = billingAddress;

			List<BasketItem> basketItems = new List<BasketItem>();
			BasketItem firstBasketItem = new BasketItem();
			firstBasketItem.Id = paymentDto.ProductId;
			firstBasketItem.Name = paymentDto.ProductName;
			firstBasketItem.Category1 = paymentDto.ProductName;
			firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
			firstBasketItem.Price = paymentDto.OrderPrice.ToString(CultureInfo.InvariantCulture);
			basketItems.Add(firstBasketItem);

			request.BasketItems = basketItems;

			var response =await CheckoutFormInitialize.Create(request, options);

			if(response.ErrorCode == null) 
			{
				var paymentResponse = new GetExternalPaymentResponseDto
				{
					ConversationId = response.ConversationId,
					PaymentUrl = response.PaymentPageUrl,
					token = response.Token,
				};
				return paymentResponse;
			}
			else
			{
				throw new PaymentFailException(response.ErrorMessage);
			}
		}
	}
}
