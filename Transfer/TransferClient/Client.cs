using Entities;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TransferClient
{
    public class Client
    {
        IPAddress ipAdr;
        Socket connector;
        IPEndPoint endPoint;

        public Client()
        {
            //ipAdr = IPAddress.Parse("10.158.5.145");  alex pc
            ipAdr = IPAddress.Loopback;
            endPoint = new IPEndPoint(ipAdr, 11000);
            connector = new Socket(ipAdr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task Start(int quantity)
        {
            Console.WriteLine("Start connection");
            await connector.ConnectAsync(endPoint);
            Console.WriteLine("Connected");
            NetworkStream stream = new NetworkStream(connector);
            await WriteData(stream, quantity);
            Console.WriteLine("Data transfered, answer received, stream will disposed");
            stream.Dispose();
        }

        async Task WriteData(NetworkStream stream, int quantity)
        {
            byte[] dataBuffer = MessagePackSerializer.Serialize(GetCalls(quantity));
            await stream.WriteAsync(dataBuffer, 0, dataBuffer.Length);
            string answer = await ReadAnswer(stream);
            Console.WriteLine("Answer from server - " + answer);
            return;
        }

        async Task<string> ReadAnswer(NetworkStream stream)
        {
            int recordLength = 0;
            byte[] buffer = new byte[2048];
            string answer = "";
            do
            {
                recordLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                answer += Encoding.UTF8.GetString(buffer, 0, recordLength);
            }
            while (stream.DataAvailable);
            return answer;
        }

        Call[] GetCalls(int quantity)
        {
            Call callEmtp = new Call()
            {
                Id = 1,
                AgentId = 4,
                CallExternalId = "5",
                CampaignId = 6,
                DialedPhoneNumber = "45454",
                EndTime = DateTime.UtcNow,
                PhoneNumber = "23123",
                StartTime = DateTime.UtcNow,
                Status = 11,
                WasSentToHub = false
            };
            Call[] calls = new Call[quantity];
            for (int i = 0; i < calls.Length; i++)
            {
                callEmtp.Id = i;
                calls[i] = callEmtp;
            }
            return calls;
        }
    }
}
