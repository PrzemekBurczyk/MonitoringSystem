﻿using System;
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
        private int clientNumber = 0;

        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }
        
        public void create(ClientConnection clientConnection, ClientObject clientObject)
        {
            clientObject.clientConnection = clientConnection;
            clientConnection.onDataReceived = clientObject.passData;

            ClientObjectCollection.Add(clientObject);
        }

        public ClientObject get(int id)
        {
            if (ClientObjectCollection.Count < id + 1)
            {
                return null;
            }

            return ClientObjectCollection[id];
        }
    }
}
