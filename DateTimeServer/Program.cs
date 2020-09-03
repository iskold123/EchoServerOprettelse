using System;

namespace DateTimeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DateServer worker = new DateServer();
            worker.Start();

            Console.ReadLine();
        }
    }
}
