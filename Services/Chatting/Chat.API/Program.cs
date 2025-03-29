using Chat.API.Hubs;
using Chat.API.Repositories;
using Marten;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
		options.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				var accessToken = context.Request.Query["access_token"];

				var path = context.HttpContext.Request.Path;
				if (!string.IsNullOrEmpty(accessToken) &&
					(path.StartsWithSegments("/chat")))
				{
					context.Token = accessToken;
				}
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddSignalR();
builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("chat/me", (ClaimsPrincipal claimsPrincipal) =>
{
	return claimsPrincipal.Claims.Select(c => new { c.Type, c.Value });
}).RequireAuthorization();
app.UseExceptionHandler(options => { });
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chat");
app.Run();

