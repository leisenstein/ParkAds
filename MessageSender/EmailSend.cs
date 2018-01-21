using RabbitMQ.Client;
using System.Text;
using Domain;
using Newtonsoft.Json;
using DataTransferObjects;

namespace MessageSender
{
    public class EmailSend
    {
        private IConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel channel;
        
        public EmailSend()
        {
            InitializeConnection();
            InitializeInfrastructure();
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
            channel.ExchangeDeclare("email.exchange", ExchangeType.Direct, true, false, null);
            channel.QueueDeclare("email.queue", true, false, false, null);
            channel.QueueBind("email.queue", "email.exchange", "email", null);
        }

        private IBasicProperties InitializeProperties()
        {
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            return properties;
        }

        public void CloseConnection()
        {
            channel.Close();
            connection.Close();
        }

        public void SendEmail(Ad ad, object reservation)
        {
            PublicationAddress address = new PublicationAddress(ExchangeType.Direct, "email.exchange", "email");
            channel.BasicPublish(address, InitializeProperties(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ConfigureMessage(ad, reservation))));
        }

        private EmailTransferObject ConfigureMessage(Ad ad, object reservation)
        { 
            return new EmailTransferObject
            {
                Ad = ad,
                Reservation = reservation
            };
        }
    }
}
