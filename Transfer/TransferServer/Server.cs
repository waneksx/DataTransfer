using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MessagePack;

namespace TransferServer
{
    public class Server
    {
        IPAddress ipAdr;
        TcpListener listener;
        IPEndPoint endPoint;

        public Server()
        {
            ipAdr = IPAddress.Loopback;
            endPoint = new IPEndPoint(ipAdr, 11000);
            listener = new TcpListener(endPoint);
            listener.Start();
            
            Listen();
        }

        private async void Listen()
        {
            Console.WriteLine("Waiting for connection");
            var client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Connected to server");
            await ReadData(client.GetStream());
            Listen();
        }

        private async Task ReadData(NetworkStream stream)
        {
            byte[] buffer = new byte[2048];
            List<byte> dataBuffer = new List<byte>();
            int recordLength = 0;

            do
            {
                recordLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                dataBuffer.AddRange(buffer.Take(recordLength));
            }
            while (stream.DataAvailable);

            Call[] transferedCalls = MessagePackSerializer.Deserialize<Call[]>(dataBuffer.ToArray());
            Console.WriteLine($"Transfered array with length = {transferedCalls.Length}");

            await WriteAnswer(stream);
            stream.Dispose();
        }

        private async Task WriteAnswer(NetworkStream stream)
        {
            byte[] answer = Encoding.UTF8.GetBytes("Data transfered succesfully");
            await stream.WriteAsync(answer, 0, answer.Length);
            return;
        }
    }
}