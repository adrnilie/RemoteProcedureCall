using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteProcedureCall
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ipAddress = IPAddress.Parse("192.168.100.8");
                var myList = new TcpListener(ipAddress, 4444);
                myList.Start();

                Console.WriteLine("Remote Procedure Call SERVER");
                Console.WriteLine($"Server is runing on {ipAddress}:4444");
                Console.WriteLine($"Local Endpoint: {myList.LocalEndpoint}");
                Console.WriteLine($"Waiting for connections...");

                var socket = myList.AcceptSocket();
                Console.WriteLine($"Connection Accepted from : {socket.RemoteEndPoint}");

                var b = new byte[100];
                var k = socket.Receive(b);

                Console.WriteLine("Recieved..");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(b[i]));
                }
                var asencd = new ASCIIEncoding();
                socket.Send(asencd.GetBytes("Automatic Message:" + "String Received byte server !"));
                Console.WriteLine("\nAutomatic Message is Sent");
                socket.Close();
                myList.Stop();
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error.." + e.StackTrace);
            }
        }
    }
}
