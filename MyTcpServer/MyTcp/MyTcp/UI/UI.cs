using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UI
{
    class UI
    {
        private static Socket clientSocket;
        private static Thread thread;
        private static Thread rthread;
        private static byte[] data = new byte[1024];
        private static bool isClose = false;
        private static bool witch = true;

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));



            while (!isClose)
            {
                Console.WriteLine("\n\n請輸入\n");
                string message = Console.ReadLine();
                thread = new Thread(Send);
                thread.IsBackground = true;
                thread.Start(message);
                thread.Join();

                rthread = new Thread(ReceveiceMessage);
                rthread.IsBackground = true;
                rthread.Start();
                rthread.Join();
            }
        }

        private static void Send(Object obj)
        {
            string message = obj as string;
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);

            
        }

        private static void ReceveiceMessage()
        {
            if (clientSocket.Connected == false) isClose = true;
            int length = clientSocket.Receive(data);
            string message = Encoding.UTF8.GetString(data, 0, length);
            Console.WriteLine("我輸出" + message);
        }
    
    }
}
