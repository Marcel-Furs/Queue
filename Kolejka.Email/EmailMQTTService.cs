using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolejka.Email
{
    internal class EmailMQTTService
    {
        private readonly string mqttUrl = "amqps://lprwjkgz:KOMvh4kaR_02BtzfORIeaReTHbMx1Ioj@cow.rmq2.cloudamqp.com/lprwjkgz";
        private IModel channel;

        public void ConsumeMessages()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(mqttUrl),
            };
            using var connection = factory.CreateConnection();
            channel = connection.CreateModel();

            string queueName = "Email";
            bool durable = true;
            bool exclusive = false;
            bool autoDelete = false;

            channel.QueueDeclare(queueName, durable, exclusive, autoDelete);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(consumer, queueName);

            ManualResetEvent resetEvent = new ManualResetEvent(false);
            resetEvent.WaitOne();
            channel.Close();
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var data = Encoding.UTF8.GetString(body);
            Console.WriteLine(data);
            channel.BasicAck(e.DeliveryTag, false);
        }
    }
}
