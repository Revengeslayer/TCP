using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace MyTcpServer
{

    class MyTcpServer
    {
        static List<TcpClient> clientList = new List<TcpClient>();
        static Dictionary<TcpClient, int> clientsList = new Dictionary<TcpClient, int>(); 
        static int userCount=0;
        static int count = 0;

        public static void BroadcastMessage(string message, TcpClient clientSend)
        {
             
            Console.WriteLine("有道公告版");
            var notConnectedList = new List<TcpClient>();
            foreach (var client in clientList)
            {
                if (!client.Connected())
                {
                    notConnectedList.Add(client);
                }
                else
                {
                    Console.WriteLine("數量    " + clientSend._clientsList.Count);
                    if (clientSend == client)
                    {
                        Console.WriteLine(clientsList[clientSend] + "    :  " + message);
                    }

                    client.SendMessage(message);                   
                }
            }
            foreach (var temp in notConnectedList)
            {
                clientList.Remove(temp);
                userCount--;
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
                userCount++;
                count++;
                var temp = (clientShocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                Console.WriteLine(temp+" "+ count + " 進入");
                TcpClient client = new TcpClient(clientShocket, count);
                clientsList.Add(client, count);
                clientList.Add(client);
                client.SetList(clientsList);
            }
            
        }
    }
}
