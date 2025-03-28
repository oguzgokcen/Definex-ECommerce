using Chat.API.Hubs;
using CommonConcerns.Exceptions.Handler;
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
	});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();

