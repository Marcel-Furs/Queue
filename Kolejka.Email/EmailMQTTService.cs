using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kolejka.Email
{
    internal class EmailMQTTService
    {
        private readonly string mqttUrl = "amqps://lprwjkgz:KOMvh4kaR_02BtzfORIeaReTHbMx1Ioj@cow.rmq2.cloudamqp.com/lprwjkgz";
        private IModel channel;
        private SmtpClient smtpClient;

        public void ConsumeMessages()
        {
            smtpClient = new SmtpClient();
            smtpClient.Host = "mail56.mydevil.net";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("rabbitmq@weresa.usermd.net", "k0qjyArXoyrEdR_9SHk2_)S6'0953U");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

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
            EmailDto emailDto = JsonSerializer.Deserialize<EmailDto>(data);

            Console.WriteLine("Wchodzi");
            try
            {
                smtpClient.Send(new MailMessage("rabbitmq@weresa.usermd.net", emailDto.Receiver)
                {
                    Body = emailDto.Body,
                    Subject = emailDto.Subject,
                });
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


            Console.WriteLine(emailDto.Receiver);

            channel.BasicAck(e.DeliveryTag, false);
        }
    }
}
