using Catalog.Models;
using Microsoft.EntityFrameworkCore;
using Catalog.Controllers;
using FluentValidation.AspNetCore;
using Catalog.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CatalogContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogContext") ?? throw new InvalidOperationException("Connection string 'CatalogContext' not found."),
		sqlOptions => sqlOptions.EnableRetryOnFailure()));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	SeedData.Initialize(services);
}

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
