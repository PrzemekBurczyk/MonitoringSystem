using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Core
{
    class MainWindowViewModel
    {
        private ClientObjectManager clientObjectManager;
        ServerCommunicationManager serverCommunicationManager;
        public ClientObjectManager ClientObjectManager
        {
            get { return clientObjectManager; }
            set { clientObjectManager = value; }
        }

        public MainWindowViewModel()
        {
            ClientObjectManager = new ClientObjectManager();
            ObservableCollection<ClientObject> clientObjectsForTests = new ObservableCollection<ClientObject> { new ClientObject("Klient1"), new ClientObject("Klient2") };
            ClientObjectManager.ClientObjectCollection = clientObjectsForTests;
            serverCommunicationManager = new ServerCommunicationManager(clientObjectManager);
            serverCommunicationManager.Start();
        }
    }
}
