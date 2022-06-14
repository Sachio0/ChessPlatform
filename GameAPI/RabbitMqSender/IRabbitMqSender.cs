namespace GameAPI.RabbitMqSender
{
    public interface IRabbitMqSender
    {
        void SendMessage(object message);
    }
}
