using System.Security.Claims;

namespace Ordering.API.Endpoints;

public class CreateOrderRequest
{
	public Guid UserId {get;set;}
	public decimal Price { get; set; }
	public List<BasketItemDto> Items { get; set; }
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
			var command = request.Adapt<CreateOrderCommand>();
			var result = await sender.Send(command);
			var response = result.Adapt<CreateOrderResponse>();
			return Results.Created($"/orders/", response);
		}).RequireAuthorization()
		.WithName("CreateOrder")
		.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Create Order")
		.WithDescription("Create Order");
	}
}

