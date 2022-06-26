using System.IO;
using System.Net;

namespace Task1Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
                return;
            if (!IPAddress.TryParse(args[0], out IPAddress ipAddress))
                return;
            if (!int.TryParse(args[1], out int port))
                return;
            if (!Directory.Exists(args[2]))
                return;

            Server server = new Server(ipAddress, port, args[2]);
            server.Start();
        }
    }
}
