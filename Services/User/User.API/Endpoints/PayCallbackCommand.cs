using MediatR;
using Ordering.API.Features.CompletePayment;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Endpoints;

public record PayCallbackRequest(string Token);

public class PayCallbackCommandEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/order/paycallback", async (
			HttpContext context, ISender sender) =>
		{
			var token = context.Request.Form["token"].ToString();
			var command = new PayCallbackCommand { token = token };
			var response = await sender.Send(command);
			var result = response.Adapt<PayCallbackResponse>();

			return Results.Redirect("http://localhost:3000/my-account/orders?success=true");
		})
		.WithName("Paycallback")
		.Produces<PayCallbackResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get User Orders")
		.WithDescription("Get all orders for the authenticated user");
	}
}
