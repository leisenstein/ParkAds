using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using DataAccess.ExternalServices;
using DataTransferObjects;
using MicroServices.Services;

namespace MessageReceiver
{
    public class EmailReceive
    {
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        public EmailReceive()
        {
            InitializeConnection();
            InitializeInfrastructure();
            InitializeConsumer();
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
                string message = Encoding.UTF8.GetString(body);
                EmailMicroService emailMicroService = new EmailMicroService();
                emailMicroService.SendSimpleMessage(JsonConvert.DeserializeObject<EmailTransferObject>(message));

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume("email.queue", false, consumer);
        }

    }
}
