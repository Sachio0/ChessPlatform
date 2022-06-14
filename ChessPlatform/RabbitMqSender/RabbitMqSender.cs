using ChessPlatform.Web.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ChessPlatform.Web.RabbitMqSender
{
    public class RabbitMqSender : IRabbitMqSender
    {
        private IConnection _connection;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private const string _rabbitMq = "RabbitMq";
        public RabbitMqSender(IConfiguration configuration)
        {
            _rabbitMqConfig = configuration.GetSection(_rabbitMq).Get<RabbitMqConfig>();
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
        public string RPCSendMessage(object message)
        {
            CreateConnectionIfNotExist();
            using var channel = _connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            channel.QueueDeclare(_rabbitMqConfig.QueueName, false, false, false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish("", _rabbitMqConfig.QueueName, null, body);
            string response = null;
            consumer.Received += (model, ea) =>
            {
                

                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    
                    //var responseBytes = Encoding.UTF8.GetBytes(response);
                    //channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                    //  basicProperties: replyProps, body: responseBytes);
                    //channel.BasicAck(deliveryTag: ea.DeliveryTag,
                    //  multiple: false);
                }
            };
            return response;
        }

    }
}
