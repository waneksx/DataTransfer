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
        Socket listener;
        IPEndPoint endPoint;

        public Server()
        {
            ipAdr = IPAddress.Parse("127.0.0.1");
            endPoint = new IPEndPoint(ipAdr, 11000);
            listener = new Socket(ipAdr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endPoint);
            listener.Listen(10);
            Listen();
        }

        private async void Listen()
        {
            Socket socket = await listener.AcceptAsync();
            await Task.Run(() => ReadData(socket));
            Listen();
        }

        private async void ReadData(Socket socket)
        {
            byte[] buffer = new byte[2048];
            List<byte> dataBuffer = new List<byte>();
            int recordLength = 0;
            NetworkStream stream = new NetworkStream(socket);

            do
            {
                recordLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                dataBuffer.AddRange(buffer.Take(recordLength));
            }
            while (stream.DataAvailable);

            Call[] transferedCalls = MessagePackSerializer.Deserialize<Call[]>(dataBuffer.ToArray());

            await WriteAnswer(stream);
            stream.Dispose();
            socket.Shutdown(SocketShutdown.Both);
            socket.Dispose();
        }

        private async Task WriteAnswer(NetworkStream stream)
        {
            byte[] answer = Encoding.UTF8.GetBytes("Data transfered succesfully");
            await stream.WriteAsync(answer, 0, answer.Length);
            return;
        }
    }
}