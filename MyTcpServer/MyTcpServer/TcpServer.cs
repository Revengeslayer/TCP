using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace MyTcpServer
{

    class MyTcpServer
    {
        static List<TcpClient> clientList = new List<TcpClient>();

        public static void BroadcastMessage(string message)
        {
            var notConnectedList = new List<TcpClient>();

            foreach (var client in clientList)
            {
                if (client.Connected())
                    client.SendMessage(message);

                notConnectedList.Add(client);
            }
            foreach (var temp in notConnectedList)
            {
                clientList.Remove(temp);
            }
        }
        static void Main(string[] args)
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            tcpServer.Listen(100);
            Console.WriteLine("server run!");


            
            while(true)
            {
                Socket clientShocket = tcpServer.Accept();
                Console.WriteLine("a client connected! ");
                TcpClient client = new TcpClient(clientShocket);
                clientList.Add(client);
            }
            
        }
    }
}
