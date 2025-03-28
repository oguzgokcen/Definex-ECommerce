using Microsoft.AspNetCore.Mvc.Diagnostics;
using Ordering.API.Data.Entities;
using Ordering.API.Repositories;

namespace Ordering.API.Features.GetUserOrders
{
    public class GetUserOrdersHandler : IQueryHandler<GetUserOrdersQuery, GetUserOrdersResponse>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetUserOrdersHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetUserOrdersResponse> Handle(GetUserOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllWhereAsync(
                o => o.UserId == query.UserId,
                cancellationToken);

            var orderDtos = orders.Select(order => new OrderDto
            {
				Price = order.Price,
                CreatedOnUtc = order.CreatedDate,
                Id = order.Id,
				BasketItems = order.OrderItems.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                }).ToList()
            }).ToList();

            return new GetUserOrdersResponse
            {
                IsSuccess = true,
                Orders = orderDtos
            };
        }
    }
} 