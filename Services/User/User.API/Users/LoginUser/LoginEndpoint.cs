namespace User.API.Users.LoginUser;

public class LoginEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/user/login", async (LoginUserCommand request, ISender sender) =>
		{
			var result = await sender.Send(request);
			var response = result.Adapt<LoginUserResult>();
			return Results.Ok(response);
		})
		.WithName("LoginUser")
		.Produces<LoginUserResult>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Login User");
	}
}


