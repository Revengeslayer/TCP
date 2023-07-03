using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTcpServer
{
    class TcpClient
    {
        private Socket clientShocket;
        private Thread thread;
        private byte[] data = new byte[1024];

        public TcpClient(Socket clientShocket)
        {
            this.clientShocket = clientShocket;
            thread = new Thread(ReceiveMessage);
            thread.Start();
        }

        internal void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientShocket.Send(data);
        }

        internal bool Connected()
        {
            return clientShocket.Connected;
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                if (clientShocket.Poll(10, SelectMode.SelectRead))
                {
                    clientShocket.Close();
                    break;
                }

                int length = clientShocket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, length);

                MyTcpServer.BroadcastMessage(message);
            }
        }
    }
}
