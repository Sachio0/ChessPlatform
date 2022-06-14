using GameAPI.Dto;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GameAPI.RabbitMqSender
{
    public class RabbitMqSender : IRabbitMqSender
    {
        private readonly RabbitMqConfig _rabbitMqConfig;
        private const string _rabbitMq = "RabbitMq";
        private IConnection _connection;
        public RabbitMqSender(IConfiguration configuration)
        {
            _rabbitMqConfig =  configuration.GetSection(_rabbitMq).Get<RabbitMqConfig>();
        }

        public void SendMessage(object message)
        {
            CreateConnectionIfNotExist();
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(_rabbitMqConfig.QueueName, false, false, false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish("", _rabbitMqConfig.QueueName, null, body);
        }
        public void CreateConnectionIfNotExist()
        {
            if (_connection != null) return;
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqConfig.Host,
                    UserName = _rabbitMqConfig.UserName,
                    Password = _rabbitMqConfig.Password
                };
                _connection = factory.CreateConnection();

            }
            catch (Exception)
            {
                //log
                throw;
            }
        }
        
    }
}
