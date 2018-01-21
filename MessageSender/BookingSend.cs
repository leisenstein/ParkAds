using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Collections.Concurrent;

namespace MessageSender
{
    public class BookingSend
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties properties;
        public BookingSend()
        {
            // Initilize connection
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();

            // Initialize architecture
            channel = connection.CreateModel();
            consumer = new EventingBasicConsumer(channel);
            channel.ExchangeDeclare("booking.exchange", ExchangeType.Direct, true, false, null);
            channel.QueueDeclare("booking.queue", true, false, false, null);
            channel.QueueBind("booking.queue", "booking.exchange", "booking", null);

            // Initialize properties
            properties = channel.CreateBasicProperties();
            replyQueueName = channel.QueueDeclare().QueueName;
            string correlationId = Guid.NewGuid().ToString();
            properties.Persistent = true;
            properties.CorrelationId = correlationId;
            properties.ReplyTo = replyQueueName;

            // EventingBasicConsumer initialization
            consumer.Received += (sender, consumerResponse) =>
            {
                // check for correlatioId
                IBasicProperties props = consumerResponse.BasicProperties;
                if (props != null && props.CorrelationId == correlationId)
                {
                    // Get message from consumer
                    string responseMessage = Encoding.UTF8.GetString(consumerResponse.Body);
                    respQueue.Add(responseMessage);
                }
                //channel.BasicAck(basicDeliveryEventArgs.DeliveryTag, false);
            };
        }

        public void CloseConnection()
        {
            channel.Close();
            connection.Close();
        }

        public string SendMessage(string message)
        {
            PublicationAddress address = new PublicationAddress(ExchangeType.Direct, "booking.exchange", "booking");
            channel.BasicPublish(address, properties, Encoding.UTF8.GetBytes(message));
            channel.BasicConsume(consumer, replyQueueName, true);
            return respQueue.Take(); 
        }
    }
}
