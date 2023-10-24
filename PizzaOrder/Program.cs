using Microsoft.OpenApi.Models;
using PizzaOrder.DATA;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
	options.AddPolicy("CORSPolicy", builder =>
	{
		builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
	});
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
	swaggerGenOptions =>
	{
		swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Picu shopu data", Version = "v1" });
	});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
	swaggerUIOptions.DocumentTitle = "Picu shopai";
	swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web api serving order model");
	swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.MapGet("/get-all-orders", async () => await OrdersRepository.GetOrdersAsync()).WithTags("order endpoints");
app.MapGet("/get-all-pizzas", async () => await OrdersRepository.GetPizzasAsync()).WithTags("order endpoints");
app.MapGet("/get-all-toppings", async () => await OrdersRepository.GetToppingsAsync()).WithTags("order endpoints");

app.MapGet("/get-order-by-id/{orderId}", async (int orderId) =>
{
	Order orderToReturn = await OrdersRepository.GetOrderByIdAsync(orderId);
	if (orderToReturn != null)
	{
		return Results.Ok(orderToReturn);
	}
	else
	{
		return Results.BadRequest();
	}
}).WithTags("order EndPoints");

app.MapPost("/create-order", async (Order orderToCreate) =>
{
	bool createSuccessful = await OrdersRepository.CreateOrderAsync(orderToCreate);

	if (createSuccessful)
	{
		return Results.Ok("create Succesfull");
	}
	else
	{
		return Results.BadRequest();
	}
}).WithTags("order EndPoints");

app.MapPut("/update-order", async (Order orderToUpdate) =>
{
	bool updateSuccessful = await OrdersRepository.UpdateOrderAsync(orderToUpdate);

	if (updateSuccessful)
	{
		return Results.Ok("updateSuccessful");
	}
	else
	{
		return Results.BadRequest();
	}
}).WithTags("order EndPoints");

app.MapDelete("/delete-order-by-id/{orderId}", async (int orderId) =>
{
	bool deleteSuccessful = await OrdersRepository.DeleteOrderAsync(orderId);

	if (deleteSuccessful)
	{
		return Results.Ok("delete successful");
	}
	else
	{
		return Results.BadRequest();
	}
}).WithTags("order EndPoints");


app.Run();
