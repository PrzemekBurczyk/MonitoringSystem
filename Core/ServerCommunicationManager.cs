using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    delegate void ClientDataReceivedDelegate(String data);

    class ServerCommunicationManager
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        ClientObjectManager clientObjectManager;

        public ServerCommunicationManager(ClientObjectManager newClientObjectManager)
        {
            clientObjectManager = newClientObjectManager;
        }

        public void Start()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            Console.WriteLine("Starting Listening");
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                Console.WriteLine("Accepted client connection");

                // Create connection object
                ClientConnection connection = new ClientConnection();
                connection.tcpClient = client;



                clientObjectManager.newObject(connection);

                Thread clientThread = new Thread(new ThreadStart(connection.HandleClientComm));
                clientThread.Start();
            }
        }        
    }
}
