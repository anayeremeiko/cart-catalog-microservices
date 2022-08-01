using Cart.WebAPI;
using Cart.WebAPI.Consumers;
using eShopServices.Services.Cart.Cart.API.DataServices;
using eShopServices.Services.Cart.Cart.API.DataServices.Interfaces;
using eShopServices.Services.Cart.Cart.API.Services;
using eShopServices.Services.Cart.Cart.API.Services.Interfaces;
using LiteDB;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<ItemChangedConsumer>();
	x.SetKebabCaseEndpointNameFormatter();

	x.UsingRabbitMq((context, config) => { 
		config.ReceiveEndpoint("catalog-item-event", e =>
		{
			e.ConfigureConsumer<ItemChangedConsumer>(context);
		});
		config.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
	});
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
	options.ApiVersionReader =
	ApiVersionReader.Combine(
	   new HeaderApiVersionReader("X-Api-Version"),
	   new QueryStringApiVersionReader("version"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddVersionedApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<SwaggerDefaultValues>();
	c.ResolveConflictingActions(c => c.Last());
});

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddSingleton<ILiteRepository>(new LiteDbRepository(connectionString));
builder.Services.AddSingleton<IDataService<eShopServices.Services.Cart.Cart.API.Models.Cart>, CartDataService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseRouting();
	app.UseEndpoints(builder => builder.MapControllers());
	app.UseSwagger();
	var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
	app.UseSwaggerUI(
		options =>
		{
			// build a swagger endpoint for each discovered API version
			foreach (var description in provider.ApiVersionDescriptions)
			{
				options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
			}
		});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
