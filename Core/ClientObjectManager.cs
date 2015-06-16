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
        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }
        
        public void create(ClientConnection clientConnection, ClientObject clientObject)
        {
            clientObject.clientConnection = clientConnection;
            clientConnection.clientObject = clientObject;
            clientConnection.onDataReceived = clientObject.passData;
            clientConnection.onDisconnect = removeClientObjectFromCollection;

            ClientObjectCollection.Add(clientObject);
        }

        public void removeClientObjectFromCollection(ClientObject clientObject) {
            ClientObjectCollection.Remove(clientObject);
        }
    }
}
