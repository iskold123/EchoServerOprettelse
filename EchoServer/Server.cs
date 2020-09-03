using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoServer
{
    class Server
    {
        public void Start()
        {
            // Opretter server, IP "ekko"
            TcpListener server = new TcpListener(IPAddress.Loopback, 3001);

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

                //Concurrent; starter socketTests hurtigere fordi der ikke er 5 sek ventetid.
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
            
            string[] strings = str.Split(' ');
            double i = double.Parse(strings[1], new CultureInfo("da-DK")); 
            double j = double.Parse(strings[2], new CultureInfo("da-DK"));


            // sender tilbage til klient
            sw.WriteLine(i+j);
            sw.Flush(); // Tømmer buffer
            //Thread.Sleep(5000);
            socket.Close();
        }
    }
}
