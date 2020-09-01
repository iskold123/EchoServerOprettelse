using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    class Server
    {
        public void Start()
        {
            // Opretter server, IP "ekko"
            TcpListener server = new TcpListener(IPAddress.Loopback, 7777);

            //starter server;
            server.Start();

            // Giver mulighed for at sende flere beskeder over samme server.
            while (true)
            {
                // Venter på klient skal lave et opkald
                // Skabelon uden while løkke, er nedenstende 3 linjer.
                TcpClient socket = server.AcceptTcpClient();
                //DoClient(socket);
                //socket.Close();

                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }


        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            // læser fra klient
            String str = sr.ReadLine();
            Console.WriteLine($"Server input: {str}");

            // sender tilbage til klient
            sw.WriteLine(str);
            sw.Flush(); // Tømmer buffer

            socket.Close();
        }
    }
}
