using RabbitMQ.Client;
using Shared;
using System.Text;
using System.Text.Json;

namespace Kolejki.API.Services
{
    public class MQTTService : IMQTTService
    {
        private readonly string mqttUrl;

        public MQTTService(IConfiguration configuration)
        {
            mqttUrl = configuration["MQTT:Url"];
        }

        public void SendEmail(string receiver, string subject, string body)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(mqttUrl),
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string queueName = "Email";
            bool durable = true;
            bool exclusive = false;
            bool autoDelete = false;

            channel.QueueDeclare(queueName, durable, exclusive, autoDelete);

            var emailDto = new EmailDto
            {
                Body = body,
                Receiver = receiver,
                Subject = subject
            };
            var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(emailDto));
            string exchangeName = "";
            channel.BasicPublish(exchangeName, queueName, body: data);
        }
    }
}
