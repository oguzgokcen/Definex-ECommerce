namespace Ordering.API.Features.GetUserOrders
{
    public class GetUserOrdersResponse
    {
        public bool IsSuccess { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
} 