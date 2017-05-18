using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Entities;

namespace RabbitConsumer
{
    class ProgramConsumer
    {
        static long transered = 0;
        static DateTime time = DateTime.Now;

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {
                Uri = "amqp://eeoeckin:H0sB6bMrzzXlGNyENalmX1C38WKgHNpK@lark.rmq.cloudamqp.com/eeoeckin",
                VirtualHost = "eeoeckin",
                UserName = "eeoeckin",
                Password = "H0sB6bMrzzXlGNyENalmX1C38WKgHNpK"
            };
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
                    transered += calls.Length;
                    Console.Beep(1400, 100);
                    Console.WriteLine($" [x] Recived JSON string with length {message.Length}, calls length {calls.Length}, totaly tarnsfered {transered} calls per {(DateTime.Now - time).ToString(@"h\:mm\:ss")} ");
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