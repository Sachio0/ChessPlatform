
using ChessPlatform.Web.Models;
using ChessPlatform.Web.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ChessPlatform.Web.RabbitMqConsumer
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IGameService _gameService;
        private IConnection _conncetion;
        private IModel _channel;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private const string _rabbitMq = "RabbitMq";

        public RabbitMqConsumer(IGameService gameService, IConfiguration configuration)
        {
            _gameService = gameService;
            _rabbitMqConfig = configuration.GetSection(_rabbitMq).Get<RabbitMqConfig>();
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfig.Host,
                UserName = _rabbitMqConfig.UserName,
                Password = _rabbitMqConfig.Password
            };
            _conncetion = factory.CreateConnection();
            _channel = _conncetion.CreateModel();
            _channel.QueueDeclare(_rabbitMqConfig.QueueName, false, false, false);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(_rabbitMqConfig.QueueName, false, consumer);
            return Task.CompletedTask;
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            var game = JsonConvert.DeserializeObject<GameDto>(content);
            HandleMessage(game).GetAwaiter().GetResult();
            _channel.BasicAck(e.DeliveryTag, false);
            
        }

        private async Task HandleMessage(GameDto? game)
        {
            if(game!= null) await _gameService.InsertOneAsync(game);
        }
    }
}
