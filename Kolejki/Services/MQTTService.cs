using RabbitMQ.Client;
using System.Text;

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

            var data = Encoding.UTF8.GetBytes($"{receiver};{subject};{body}");
            string exchangeName = "";
            channel.BasicPublish(exchangeName, queueName, body: data);
        }
    }
}
