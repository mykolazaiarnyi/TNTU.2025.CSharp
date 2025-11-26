using Microsoft.EntityFrameworkCore;
using TNTU.ToDoApp.API.Middlewares;
using TNTU.ToDoApp.Data;
using TNTU.ToDoApp.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ToDoItemsService>();
builder.Services.AddScoped<ICurrentUserService, MockCurrentUserService>();
builder.Services.AddDbContext<ToDoContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
