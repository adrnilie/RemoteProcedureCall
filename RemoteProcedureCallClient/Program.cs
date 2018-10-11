using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProcedureCallClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var Tcpclient = new TcpClient();

                Console.WriteLine("Connecting...");
                Console.WriteLine("Enter ip: ");
                var ip = Console.ReadLine().ToString();
                Tcpclient.Connect(ip, 4444);
                Console.WriteLine("Connected");
                Console.WriteLine("Ente the String you want to send ");

                var str = Console.ReadLine();
                var stm = Tcpclient.GetStream();
                var ascnd = new ASCIIEncoding();
                var ba = ascnd.GetBytes(str ?? throw new InvalidOperationException());

                Console.WriteLine("Sending..");
                stm.Write(ba, 0, ba.Length);

                var bb = new byte[100];
                var k = stm.Read(bb, 0, 100);
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(bb[i]));
                }

                Tcpclient.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.StackTrace);
            }
        }
    }
}
