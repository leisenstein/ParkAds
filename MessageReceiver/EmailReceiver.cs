using RabbitMQ.Client;
using System.Text;
using Domain;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using DataAccess.ExternalServices;

namespace MessageReceiver
{
    public class EmailReceiver
    {
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        public EmailReceiver()
        {
            InitializeConnection();

        }

        private void InitializeConnection()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
        }

        private void InitializeInfrastructure()
        {
            channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
        }

        private void InitializeConsumer()
        {
            consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                string[] messageStringParts = SplitString(message);
                EmailService.SendSimpleMessage(messageStringParts[0], messageStringParts[1]);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume("email.queue", false, consumer);
        }

        private string[] SplitString(string message)
        {
            return message.Split(new string[] { "divider" }, StringSplitOptions.None);
        }
    }
}
