using Ordering.API.DTOs;

namespace Ordering.API.Services
{
	public interface IPaymentService
	{
		Task<GetExternalPaymentResponseDto> Pay(CreatePaymentDto paymentDto, CancellationToken cancellationToken);
	}
}
