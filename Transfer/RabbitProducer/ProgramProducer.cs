using RabbitMQ.Client;
using System;
using System.Text;
using Entities;

namespace RabbitProducer
{
    class ProgramProducer
    {
        static int cycle = 0;
        static long transered = 0;
        static DateTime time = DateTime.Now;
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                Uri = "amqp://eeoeckin:H0sB6bMrzzXlGNyENalmX1C38WKgHNpK@lark.rmq.cloudamqp.com/eeoeckin",
                VirtualHost = "eeoeckin",
                UserName = "eeoeckin",
                Password = "H0sB6bMrzzXlGNyENalmX1C38WKgHNpK"
            };
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
                    var calls = CallJson.GetCalls();
                    string message = CallJson.CallsToJson(calls);
                    transered += calls.Length;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "call",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent JSON string with length {message.Length}, calls length {calls.Length}, totaly tarnsfered {transered} calls per {(DateTime.Now - time).ToString(@"h\:mm\:ss")} ");
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}