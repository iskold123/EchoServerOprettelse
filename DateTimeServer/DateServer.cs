using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeServer
{
    class DateServer
    {
        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 3003);

            server.Start();

            TcpClient socket = server.AcceptTcpClient();

            while (true)
            {
                
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

            String str = sr.ReadLine();

            var dt = str.Split('&');

            try
            {
                var i = DateTime.ParseExact(dt[1], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                sw.WriteLine("Valid");
            }
            catch (Exception e)
            {
                sw.WriteLine("Not Valid");
                Console.WriteLine(e);
            }

            sw.Flush();

            /* Kan undlades, hvis man ønsker at kunne sende
            * flere beskeder af gangen i SocketTest
            * send følgende til SocketTest for test; Timestamp&2019-09-03 14:23
            */
            socket.Close();
        }
    }
}
