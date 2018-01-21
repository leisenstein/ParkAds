using Domain;
using MicroServices.FactoryReservation;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageReceiver
{
    public class BookingReceive
    {
        private ConnectionFactory connectionFactory;
        private IConnection connection;
        private IModel channel;
        public BookingReceive()
        {
            CreateConnection();
        }

        private void CreateConnection()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            InitializeInfrastructure();
            InitializeConsumer();
        }
        private void InitializeInfrastructure()
        {
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
        }

        private void InitializeConsumer()
        {
            EventingBasicConsumer eventingBasicConsumer = new EventingBasicConsumer(channel);

            eventingBasicConsumer.Received += (sender, basicDeliveryEventArgs) =>
            {
                // Get message from sender
                string message = Encoding.UTF8.GetString(basicDeliveryEventArgs.Body);
                channel.BasicAck(basicDeliveryEventArgs.DeliveryTag, false);

                // Send message go booking microservice
                BookingMicroService bookingMicroService = new BookingMicroService();
                Booking booking = bookingMicroService.Add(JsonConvert.DeserializeObject<Booking>(message));

                // Send response message
                IBasicProperties replyBasicProperties = channel.CreateBasicProperties();
                replyBasicProperties.CorrelationId = basicDeliveryEventArgs.BasicProperties.CorrelationId;
                byte[] responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(booking));
                channel.BasicPublish("", basicDeliveryEventArgs.BasicProperties.ReplyTo, replyBasicProperties, responseBytes);
            };

            channel.BasicConsume("booking.queue", false, eventingBasicConsumer);
        }
    }
}
