
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder => builder.WithOrigins("http://localhost:3000")
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials());
});
var app = builder.Build();

app.MapReverseProxy();
app.UseCors("AllowSpecificOrigin");
app.Run();