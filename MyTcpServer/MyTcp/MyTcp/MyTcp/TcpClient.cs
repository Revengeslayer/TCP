using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTcpServer
{
    class TcpClient
    {
        public Socket clientShocket;
        private Thread thread;
        public int num;
        public Dictionary<TcpClient, int> _clientsList = new Dictionary<TcpClient, int>();
        private byte[] data = new byte[1024];

        public TcpClient(Socket clientShocket,int i)
        {
            num = i;
            this.clientShocket = clientShocket;
            thread = new Thread(ReceiveMessage);
            thread.IsBackground = true;
            thread.Start();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientShocket.Send(data);
        }

        public bool Connected()
        {
            return clientShocket.Connected;
        }
        private void ReceiveMessage()
        {
            bool isThreadStop=false;
            Console.WriteLine(isThreadStop);

            while (!isThreadStop)
            {

                try
                {
                    int length = clientShocket.Receive(data);
                    string message = Encoding.UTF8.GetString(data, 0, length);

                    MyTcpServer.BroadcastMessage(message, this);
                }
                catch (SocketException e)
                {
                    var temp = (clientShocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                    Console.WriteLine(temp + "  " + _clientsList[this] + "離開");
                    _clientsList.Remove(this);
                    this.clientShocket.Close();
                    isThreadStop = true;
                }

            }          

        }

        public void SetList(Dictionary<TcpClient, int> clientsList)
        {
            _clientsList = clientsList;
        }

        internal void test()
        {
            Console.WriteLine("內容物  "+_clientsList.Count);          
        }
    }
}
