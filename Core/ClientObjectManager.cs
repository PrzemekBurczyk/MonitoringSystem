using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class ClientObjectManager
    {
        protected List<ClientObject> clientObjectList = new List<ClientObject>();

        public void newObject(ClientConnection clientConnection)
        {
            ClientObject clientObject = new ClientObject();
            clientObject.clientConnection = clientConnection;

            clientConnection.onDataReceived = clientObject.passData;

            clientObjectList.Add(clientObject);
        }

        public ClientObject getObject(int id)
        {
            return clientObjectList[id];
        }
    }
}
