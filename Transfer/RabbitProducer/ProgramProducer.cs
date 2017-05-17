using RabbitMQ.Client;
using System;
using System.Text;
using Entities;

namespace RabbitProducer
{
    class ProgramProducer
    {
        static int cycle = 0;
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", VirtualHost = "fr", UserName = "alex", Password = "alex" };
            using (var connection = factory.CreateConnection("alex"))
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "call",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                while (true)
                {
                    cycle++;
                    var calls = CallJson.GetCalls();
                    string message = CallJson.CallsToJson(calls);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "call",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent JSON string with length {0}, calls length {1}", message, calls.Length);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}