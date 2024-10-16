using Store.Business.Services;
using Store.Business.Services.Interfaces;
using Store.Data.Repositories.Iterfaces;
using Store.Data.Repositories;
using Store.Business.MapperConfiguration;
using Stope.Api.MapperConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperModelProfile), typeof(MapperViewModelProfile));

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



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
