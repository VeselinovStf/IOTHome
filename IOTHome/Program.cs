using EmailClient;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IOTHome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "lora",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(message);

                await EmailSender.Execute(message);
            };

            channel.BasicConsume(queue: "lora",
                            autoAck: true,
                            consumer: consumer);

            Console.ReadKey();
        }
    }
}