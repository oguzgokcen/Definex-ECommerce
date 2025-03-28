using CommonConcerns.CQRS;

namespace Ordering.API.Features.GetUserOrders
{
    public record GetUserOrdersQuery : IQuery<GetUserOrdersResponse>
    {
        public Guid UserId { get; init; }
    }
} 