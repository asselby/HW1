using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        { 
            string message = Console.ReadLine();

            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4455);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(ipEndPoint);

            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);

            byte[] buffer = new byte[1024];
            int bytesRec = sender.Receive(buffer);
            string response = Encoding.ASCII.GetString(buffer);
            Console.WriteLine(response);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            Console.Read();
        }
    }
}
