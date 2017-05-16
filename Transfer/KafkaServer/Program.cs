﻿using RdKafka;
using System;
using System.Text;

namespace KafkaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Config() { GroupId = "example-csharp-consumer" };
            using (var consumer = new EventConsumer(config, "127.0.0.1:9092"))
            {
                consumer.OnMessage += (obj, msg) =>
                {
                    string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
                    Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
                };

                consumer.Subscribe(new System.Collections.Generic.List<string> { "test" });
                consumer.Start();

                Console.WriteLine("Started consumer, press enter to stop consuming");
                Console.ReadLine();
            }
        }
    }
}