using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        public string UserName { get; set; }
        TcpClient client;
        TCPServer server; // объект сервера

        public ClientObject(TcpClient tcpClient, TCPServer serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            // serverObject.AddConnection(this);
        }

        public bool StartAdmin()
        {
            try
            {
                Stream = client.GetStream();
                // получаем имя пользователя
                var command = GetMessage();
                var message = command.Nickname;
                UserName = command.Nickname;

                if (command.UserStatus == "Admin") return true;

                message = UserName + " вошел в чат";
                // посылаем сообщение о входе в чат всем подключенным пользователям
                server.BroadcastMessage(command, this.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task Process()
        {
            await Task.Run(() =>
            {
                try
                {
                    Stream = client.GetStream();
                    // получаем имя пользователя
                    var command = GetMessage();
                    var message = command.Nickname;
                    UserName = command.Nickname;

                    message = UserName + " вошел в чат";
                    // посылаем сообщение о входе в чат всем подключенным пользователям
                    server.BroadcastMessage(command, this.Id);
                    Console.WriteLine(message);
                    Console.WriteLine(command.Nickname + ": " + command.Message);
                    // в бесконечном цикле получаем сообщения от клиента
                    while (true)
                    {
                        try
                        {
                            command = GetMessage();
                            message = String.Format("{0}: {1}", command.Nickname, command.Message);
                            Console.WriteLine(message);
                            server.BroadcastMessage(command, this.Id);
                        }
                        catch
                        {
                            message = String.Format("{0}: покинул чат", UserName);
                            Console.WriteLine(message);
                            server.BroadcastMessage(command, this.Id);
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    // в случае выхода из цикла закрываем ресурсы
                    server.RemoveConnection(this.Id);
                    Close();
                }
            });

        }

        public void SendCommand(Command command)
        {
            try
            {
                var stream = client.GetStream();
                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command));
                do
                {
                    stream.Write(bytes, 0, bytes.Length);
                } while (stream.DataAvailable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // чтение входящего сообщения и преобразование в строку
        private Command GetMessage()
        {
            Command command = new Command();
            byte[] bytes = new byte[2048]; // буфер для получаемых данных
            do
            {
                int countb = Stream.Read(bytes, 0, bytes.Length);
                var str = Encoding.UTF8.GetString(bytes, 0, countb);
                command = JsonConvert.DeserializeObject<Command>(str);
            }
            while (Stream.DataAvailable);

            return command;
        }

        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
