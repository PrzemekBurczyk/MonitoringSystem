using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClientObjectManager
    {
        //public List<ClientObject> ClientObjectList = new List<ClientObject>();
        private int clientNumber = 0;

        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }
        public void newObject(ClientConnection clientConnection)
        {
            ClientObject clientObject = new ClientObject("Client" + clientNumber++);
            clientObject.clientConnection = clientConnection;

            clientConnection.onDataReceived = clientObject.passData;

            //ClientObjectList.Add(clientObject);
            ClientObjectCollection.Add(clientObject);
        }

        public ClientObject getObject(int id)
        {
            if (ClientObjectCollection.Count < id + 1)
            {
                return null;
            }

            return ClientObjectCollection[id];
        }
    }
}
