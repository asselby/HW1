using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 4455);

            Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(endPoint);
            listener.Listen(5);

            Socket socket = listener.Accept();

            byte[] byffer = new byte[1024];
            socket.Receive(byffer);

            string data = Encoding.UTF8.GetString(byffer, 0, byffer.Length);

            Console.Write(data);

            byte[] msg = Encoding.UTF8.GetBytes("Response! " + data);
            socket.Send(msg);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Console.Read();
        }
    }
}
