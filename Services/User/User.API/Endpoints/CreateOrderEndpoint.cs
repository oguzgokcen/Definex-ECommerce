using Ordering.API.Features.CreateOrder;
using System.Security.Claims;

namespace Ordering.API.Endpoints;

public class CreateOrderRequest
{
	public Guid UserId {get;set;}
	public decimal Price { get; set; }
	public string UserEmail { get; set; }
	public List<BasketItemDto> Items { get; set; }
	public string UserName { get; set; }
}

public class CreateOrderEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/order/create", async (CreateOrderRequest request, ISender sender, ClaimsPrincipal user) =>
		{
			var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid.TryParse(userIdString,out Guid userId);
			request.UserId = userId;
			request.UserEmail = user.FindFirstValue(ClaimTypes.Email)!;
			request.UserName = user.FindFirstValue("name")!;
			var command = request.Adapt<CreateOrderCommand>();
			var result = await sender.Send(command);
			var response = result.Adapt<CreateOrderResponse>();
			return Results.Ok(response);
		}).RequireAuthorization()
		.WithName("CreateOrder")
		.Produces<CreateOrderResponse>(StatusCodes.Status200OK)
		.WithSummary("Create Order")
		.WithDescription("Create Order");
	}
}

