using Stope.Api.MapperConfiguration;
using Stope.Api.Models;
using Store.Business;
using Store.Business.MapperConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperModelProfile), typeof(MapperViewModelProfile));

var connectionString = builder.Configuration.GetConnectionString("BookStore");

Configuration.Configure(builder.Services, connectionString);

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
