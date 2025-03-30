using CommonConcerns.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ordering.API.Data;
using System.Security.Claims;
using Ordering.API.Services;

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
builder.Services.AddDbContext<OrderDbContext>((sp,options)=>{
	options.UseNpgsql(builder.Configuration.GetConnectionString("Database")!);
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<IyzicoOptions>(builder.Configuration.GetSection("Iyzico"));
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
app.MapGet("order/me", (ClaimsPrincipal claimsPrincipal) =>
{
	return claimsPrincipal.Claims.Select(c => new { c.Type, c.Value });
}).RequireAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
