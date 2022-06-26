using System;
using System.Net.Sockets;
using System.Text;

namespace Task1Server
{
    internal class MySocket : IDisposable
    {
        public MySocket(Socket socket)
        {
            this.socket = socket;
        }


        private readonly Socket socket;

        public string Recive()
        {
            var buffer = new byte[256];
            int size = 0;
            try
            {
                size = socket.Receive(buffer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        public void Send(byte[] data)
        {
            socket.Send(data);
        }
        public void Dispose()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
