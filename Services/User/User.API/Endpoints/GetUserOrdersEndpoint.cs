using System.Security.Claims;
using Ordering.API.Features.GetUserOrders;

namespace Ordering.API.Endpoints
{
    public class GetUserOrdersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/order/myorders", async (ISender sender, ClaimsPrincipal user) =>
            {
                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                Guid.TryParse(userIdString, out Guid userId);

                var query = new GetUserOrdersQuery { UserId = userId };
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .RequireAuthorization()
            .WithName("GetUserOrders")
            .Produces<GetUserOrdersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get User Orders")
            .WithDescription("Get all orders for the authenticated user");
        }
    }
} 