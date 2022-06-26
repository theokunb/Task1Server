using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Task1Server
{
    public class Server
    {
        public Server(IPAddress ip,int port,string path)
        {
            this.ip = ip;
            this.port = port;
            this.path = path;
        }

        private readonly IPAddress ip;
        private readonly int port;
        private readonly string path;
        private FileSaver fileSaver;

        public void Start()
        {
            using(var tcpCLient = new MyTcpClient(ip, port))
            {
                while (true)
                {
                    Console.WriteLine("waiting connections");
                    using(var listener = tcpCLient.AcceptClient())
                    {
                        string fileName = listener.Recive();
                        listener.Send(Encoding.UTF8.GetBytes("yeah i got file name"));
                        int udpPort = Convert.ToInt32(listener.Recive());
                        listener.Send(Encoding.UTF8.GetBytes("yeah i got udp port"));

                        fileSaver = new FileSaver(path, fileName);

                        List<byte[]> listBytes = new List<byte[]>();
                        using (var udpCLient = new MyUdpClient(ip, udpPort))
                        {
                            while (true)
                            {
                                var buffer = new byte[256];
                                var size = udpCLient.Recive(ref buffer);

                                var response = Encoding.UTF8.GetString(buffer,0,size);

                                listener.Send(Encoding.UTF8.GetBytes("accepted"));

                                if (response == "all done")
                                    break;
                                listBytes.Add(buffer);
                            }
                        }
                        fileSaver.Save(listBytes);
                    }
                }
            }
        }
    }
}
