using Catalog.API.Services;
using Catalog.API.Services.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Services;
using Catalog.Core.Validators;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Entities;
using Catalog.SharedKernel.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
});
builder.Services.AddScoped(typeof(Catalog.SharedKernel.Interfaces.IReadRepository<Item>), typeof(ItemsReadRepository));
builder.Services.AddScoped(typeof(IRepository<Item>), typeof(ItemsRepository));
builder.Services.AddScoped(typeof(IRepository<Category>), typeof(CategoriesRepository));
builder.Services.AddScoped(typeof(Catalog.SharedKernel.Interfaces.IReadRepository<Category>), typeof(CategoriesReadRepository));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICategoryService).Assembly);
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});
builder.Services.AddAutoMapper(typeof(EntitiesProfile));

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>());
builder.Services.AddFluentValidationRulesToSwagger();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
