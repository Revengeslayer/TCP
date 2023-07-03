using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            tcpSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            tcpSocket.Listen(100);

            while(true)
            {
                Socket clientSocket = tcpSocket.Accept();
                Console.WriteLine("等待接受");
            }
        }
    }
}
