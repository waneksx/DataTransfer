using RdKafka;
using System;
using System.Text;
using System.Threading.Tasks;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task act = new Task(Start);
            act.Start();
            Console.ReadLine();
        }

        private static async void Start()
        {
            using (Producer producer = new Producer("127.0.0.1:9092"))
            using (Topic topic = producer.Topic("test"))
            {
                byte[] data = Encoding.UTF8.GetBytes("Hello RdKafka");
                DeliveryReport deliveryReport = await topic.Produce(data);
                Console.WriteLine($"Produced to Partition: {deliveryReport.Partition}, Offset: {deliveryReport.Offset}");
            }
        }
    }
}