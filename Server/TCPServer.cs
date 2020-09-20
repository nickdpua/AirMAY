using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class TCPServer
    {
        public delegate Task EventStarts();
        public event EventStarts EventStart;

        private TcpListener tcpListener; // сервер для прослушивания
        private List<ClientObject> clients = new List<ClientObject>(); // все подключения
        private List<ChatClientObject> chatClientObjects = new List<ChatClientObject>();
        private ClientObject _adminClientObject;
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }
        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                do
                {
                    TcpClient tcpClientAdmin = tcpListener.AcceptTcpClient();

                    ClientObject clientObjectAdmin = new ClientObject(tcpClientAdmin, this);
                    EventStart += clientObjectAdmin.Process;
                    _adminClientObject = clientObjectAdmin;
                } while (_adminClientObject.StartAdmin());

                EventStart.Invoke();

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    EventStart += clientObject.Process;
                    clients.Add(clientObject);

                    EventStart.Invoke();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }


        protected internal void BroadcastMessage(Command message, string id)
        {
            if (chatClientObjects.FirstOrDefault(x => x.ClientObject.Id == id) is ChatClientObject chatClientObject)
            {
                chatClientObject.BroadcastMessage(message, id);
            }
            else
            {
                chatClientObjects.Add(new ChatClientObject()
                {
                    ChatId = _adminClientObject.Id + id,
                    AdminClientObject = _adminClientObject,
                    ClientObject = clients.First(x => x.Id == id)
                });
            }


            //foreach (var item in clients)
            //{
            //    if (item.Id != id) item.SendCommand(message);
            //}
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }

    }
}
