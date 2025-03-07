﻿using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;

namespace SubscriberAPI.Application.RabbitQM
{
    public class RabbitMessageProducer : IRabbitMessageProducer
    {
        public void ConsumingSubscriberMessag<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "rmq",
                Password = "pass",
                VirtualHost = "/"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("ProductApi",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" The message has been received {message}");
            };
            channel.BasicConsume(queue: "ProductApi", autoAck: true, consumer: consumer);
        }


        public void SendProductSearchMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "rmq",
                Password = "pass",
                VirtualHost = "/"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("TelegramBotAPI",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            var JsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(JsonString);
            channel.BasicPublish(exchange: "", routingKey: "TelegramBotAPI", body: body);
        }
    }
}
