using System;
using System.Net;
using System.Net.Sockets;

namespace TcpClinet
{  
    class Client
    {
        private static Socket clientSocket;
        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"),8080));
        }
    }
}
