namespace ChessPlatform.Web.RabbitMqSender
{
    public interface IRabbitMqSender
    {
        void SendMessage(object message);
    }
}
