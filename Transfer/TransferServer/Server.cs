using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TransferServer
{
    public class Server
    {
        IPAddress ipAdr;
        Socket listener;

        public Server()
        {
            ipAdr = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ipAdr, 11000);
            listener = new Socket(ipAdr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endPoint);
            listener.Listen(10);
            Listen();
        }

        private async void Listen()
        {
            Socket socket = await listener.AcceptAsync();
            TransferData(socket);
            Listen();
        }

        private async void TransferData(Socket socket)
        {
            byte[] buffer = new byte[2048];
            NetworkStream stream = new NetworkStream(socket);
        }
    }
}