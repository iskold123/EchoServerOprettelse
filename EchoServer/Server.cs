using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

            // Venter på klient skal lave et opkald
            TcpClient socket = server.AcceptTcpClient();

            ////Har lagt linje 24-33 i en metoden "DoClient"

            //StreamReader sr = new StreamReader(socket.GetStream());
            //StreamWriter sw = new StreamWriter(socket.GetStream());

            // læser fra klient
            //String str = sr.ReadLine();
            //Console.WriteLine($"Server input: {str}");

            // sender tilbage til klient
            //sw.WriteLine(str);
            //sw.Flush(); // Tømmer buffer

            DoClient(socket);

            socket.Close();

        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());

            String str = sr.ReadLine();
            Console.WriteLine($"Server input: {str}");

            sw.WriteLine(str);
            sw.Flush(); // Tømmer buffer
        }
    }
}
