using System;
using Domain;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace MessageSender
{
    public class AdSend
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<Ad> respQueue = new BlockingCollection<Ad>();
        private readonly IBasicProperties properties;
        public AdSend()
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
            channel.ExchangeDeclare("ad.exchange", ExchangeType.Direct, true, false, null);
            channel.QueueDeclare("ad.queue", true, false, false, null);
            channel.QueueBind("ad.queue", "ad.exchange", "ad", null);

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
                    respQueue.Add(JsonConvert.DeserializeObject<Ad>(responseMessage));
                }
                //channel.BasicAck(basicDeliveryEventArgs.DeliveryTag, false);
            };
        }

        public void CloseConnection()
        {
            channel.Close();
            connection.Close();
        }

        public Ad AdRequest()
        {
            PublicationAddress address = new PublicationAddress(ExchangeType.Direct, "ad.exchange", "ad");
            channel.BasicPublish(address, properties, Encoding.UTF8.GetBytes("Get ad"));
            channel.BasicConsume(consumer, replyQueueName, true);
            return respQueue.Take();
        }
    }
}