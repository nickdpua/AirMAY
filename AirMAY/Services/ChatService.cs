using AirMAY.Services.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AirMAY.Services
{
    public class ChatService
    {
        private TcpClient _tcpClient;

        public List<ChatHistory> ChatHistories { get; set; }
        public List<string> ChatIdList { get; set; }
        public string CurrentTag { get; set; }

        public delegate Task EventResiveCommand(Command command);
        public event EventResiveCommand CommandReciveEvent;

        public ChatService()
        {
            _tcpClient = new TcpClient();
            ChatIdList = new List<string>();
            ChatHistories = new List<ChatHistory>();
        }
        public void Start()
        {
            try
            {
                //_tcpClient.Connect(new IPEndPoint(IPAddress.Parse("192.168.56.1"), 8888));
                _tcpClient.Connect(new IPEndPoint(IPAddress.Parse("212.1.85.248"), 8888));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static IPAddress GetIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }
        public void SendCommand(Command command)
        {
            try
            {
                var stream = _tcpClient.GetStream();
                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command));


                stream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task<Command> ReadCommand()
        {
            while (true)
            {
                try
                {
                    var stream = _tcpClient.GetStream();

                    Command commandresult = new Command();
                    do
                    {
                        var bytes = new byte[2048];
                        if (stream.CanRead == true)
                        {
                            int countb = await stream.ReadAsync(bytes, 0, bytes.Length);
                            var str = Encoding.UTF8.GetString(bytes, 0, countb);
                            commandresult = JsonConvert.DeserializeObject<Command>(str);
                        }
                    } while (stream.DataAvailable);
                    CommandReciveEvent?.Invoke(commandresult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    break;
                }
            }
            return null;
        }
    }
}
