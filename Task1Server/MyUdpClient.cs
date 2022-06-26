using System;
using System.Net;
using System.Net.Sockets;

namespace Task1Server
{
    internal class MyUdpClient : IDisposable
    {
        public MyUdpClient(IPAddress ip,int port)
        {
            EndPoint endPoint = new IPEndPoint(ip, port);
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(endPoint);
        }



        private readonly Socket udpSocket;




        public int Recive(ref byte[] buffer)
        {
            try
            {
                EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                return udpSocket.ReceiveFrom(buffer, ref endPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public void Dispose()
        {
            udpSocket.Close();
        }
    }
}
