using JasperFx.CodeGeneration.Frames;
using System.Text;
using System.Text.Json;
using User.API.Exceptions;

namespace User.API.Users.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand<LoginUserResult>;

public record LoginUserResult(string Token);

public class LoginUserAbstractValidator : AbstractValidator<LoginUserCommand>
{
	public LoginUserAbstractValidator()
	{
		RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
		RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
	}
}
public class LoginHandler: ICommandHandler<LoginUserCommand, LoginUserResult>
{
	public async Task<LoginUserResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
	{
		using (var client = new HttpClient())
		{
			var keycloakUrl = "http://localhost:8080/realms/e-commerce/protocol/openid-connect/token";
			var clientId = "public-client2";
			var username = command.Email;
			var password = command.Password;
			var clientSecret = "HSHBbbBVj3BRM6jJoAyuPKMGtHVI0a7Y";
			var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);
			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("grant_type", "password"),
				new KeyValuePair<string, string>("client_id", clientId),
				new KeyValuePair<string, string>("scope", "email openid"),
				new KeyValuePair<string, string>("username", username),
				new KeyValuePair<string, string>("password", password)
			});

			var response = await client.PostAsync(keycloakUrl, content);
			if (!response.IsSuccessStatusCode)
			{
				throw new AuthFailedException();
			}
			var responseString = await response.Content.ReadAsStringAsync();
			var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);

			var accessToken = jsonResponse.GetProperty("access_token").GetString();
			return new LoginUserResult(accessToken!);
		}
	}
}

