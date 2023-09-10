using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TodoWebService;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Validations;
using TodoWebService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
