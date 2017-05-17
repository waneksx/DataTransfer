using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Entities;

namespace RabbitConsumer
{
    class ProgramConsumer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { UserName = "ivan", Password = "ivan", VirtualHost = "fr", HostName = "10.158.5.145" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "call",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var calls = CallJson.JsonToCalls(message);
                    Console.WriteLine(" [x] Received JSON string with length {0}, call array length {1}", message.Length, calls.Length);
                };
                channel.BasicConsume(queue: "call",
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}