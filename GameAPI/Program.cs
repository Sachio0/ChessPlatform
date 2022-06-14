using GameAPI.AutoMapper;
using GameAPI.RabbitMqConsumer;
using GameAPI.Services;
using GameAPI.Services.Interfaces;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<MongoClientBase>(new MongoClient(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton(ServiceProfile.RegiserMaps().CreateMapper());
builder.Services.AddHostedService<RabbitMqConsumer>();
builder.Services.AddSingleton<IGameService, GameService>();
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
