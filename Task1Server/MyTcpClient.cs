using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Task1Server
{
    internal class MyTcpClient : IDisposable
    {
        public MyTcpClient(IPAddress ip,int port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(ip, port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(iPEndPoint);
            tcpSocket.Listen(5);
        }


        private readonly Socket tcpSocket;


        public MySocket AcceptClient()
        {
            return new MySocket(tcpSocket.Accept());
        }

        public void Dispose()
        {
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
