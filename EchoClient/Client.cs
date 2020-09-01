using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    class Client
    {
        public void Start()
        {
            //Opretter client
            TcpClient socket = new TcpClient("localhost", 7777);


            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            String str = sr.ReadLine();
            Console.WriteLine($"Client into" +  str);

            sw.WriteLine(str);
            sw.Flush(); // Tømmer buffer



        }
    }
}
