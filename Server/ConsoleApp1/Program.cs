using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    class Program
    {

        /********************************************************************
         * 
         *  each client should have its own object in the server so that we know to which client the data should be sent
         *  
         *  and is there any way to create a tcp client in Javascript?
         *
         *  please help
         */


        static void Main(string[] args)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8080);
            TcpClient client = default(TcpClient);

            string sms = "recieved";

            try
            {
                server.Start();
                Console.WriteLine("Server Started");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error!");
                Console.WriteLine(ex.ToString());
                Console.Read();
            }
            while (true)
            {
                client = server.AcceptTcpClient();
                byte[] recievedData = new byte[100];


                int byteCount = Encoding.ASCII.GetByteCount(sms);
                byte[] sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(sms);


                NetworkStream stream = client.GetStream();
                
                

                stream.Read(recievedData, 0, recievedData.Length);
                stream.Write(sendData,0,sendData.Length);

                StringBuilder msg = new StringBuilder();

                foreach (byte b in recievedData)
                {
                    if (b.Equals(00))
                    {
                        break;
                    }
                    else
                        msg.Append(Convert.ToChar(b).ToString());
                }

                Console.WriteLine(msg.ToString() + msg.Length);
            }
        }
    }
}
