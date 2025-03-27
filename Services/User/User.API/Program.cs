using CommonConcerns.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCarter();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Audience = builder.Configuration["Authentication:Audience"];
		options.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"]!;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = builder.Configuration["Authentication:ValidIssuer"]
		};
		options.RequireHttpsMetadata = false;
	});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
{
	return claimsPrincipal.Claims.Select(c => new { c.Type, c.Value });
}).RequireAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
