using ChessPlatform.Web.RabbitMqSender;
using ChessPlatform.Web.Services;
using ChessPlatform.Web.Services.Interfaces;
using ChessPlatform.Web.Statics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRabbitMqSender, RabbitMqSender>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddSingleton(builder.Configuration);
Url.GetInstance().AddCall(ChessPlatform.Web.Enums.ApiCall.Game, builder.Configuration.GetValue<string>("ServiceUrls:GameAPI"));
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
