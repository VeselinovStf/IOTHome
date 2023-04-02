using EmailClient;
using EmailClient.Models.Email;
using IOTHome.Config;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IOTHome
{
    public class Program
    {
        // Target Addresses
        private static readonly List<EmailSendModel> emailSendModels = new List<EmailSendModel>()
            {
                new EmailSendModel()
                {
                     To = "chofexx@gmail.com",
                     PlainTextContent = "Lora Device Activiti Was Detected",
                     Subject = "Lora Activity",
                     ToName = "Veselinov Stf",
                     HTMLContent = $"<strong>Movement Has Been Detected</strong>"
                }
            };


        public static void Main(string[] args)
        {
            var configuration = new MessegeQuerySetting();
            var emailSender = new EmailSender(new EmailSenderConfig());

            // Configure MQ
            var factory = new ConnectionFactory
            {
                HostName = configuration.HostName,
                UserName = configuration.UserName,
                Password = configuration.Password,
                Port = configuration.Port
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: configuration.RoutingKey,
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            // Read Message
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message Received: {message}");

                // Send EMail
                var mailResult = await emailSender.Execute(emailSendModels);
                Console.WriteLine(mailResult.Message);
            };
            channel.BasicConsume(queue: configuration.RoutingKey,
                            autoAck: true,
                            consumer: consumer);

            // Block
            while (true) { }
        }
    }
}