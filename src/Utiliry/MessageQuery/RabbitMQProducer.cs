using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageQuery
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ConnectionFactory _factory;
        private string _routingKey;

        public RabbitMQProducer(MQSettings mQSettings)
        {
            this._factory = new ConnectionFactory
            {
                HostName = mQSettings.HostName,
                UserName = mQSettings.UserName,
                Password = mQSettings.Password,
                Port = mQSettings.Port
            };

            this._routingKey = mQSettings.RoutingKey;
        }

        public void SendMessage<T>(T message)
        {
            var connection = this._factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _routingKey,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _routingKey, body: body);
        }
    }
}
