using System;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static TCPServer server; // сервер
        static void Main(string[] args)
        {
            try
            {
                server = new TCPServer();
                Task.Factory.StartNew(() => { server.Listen(); });
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

        }
    }
}