using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Components.Console;
using Components.Gauge;
using Components.PieChart;
using Components.RichConsole;
using Components.TimeChart;
using GuiComponentInterfaces;
using Console = Components.Console.Console;

namespace Core
{
    class MainWindowViewModel
    {
        private ClientObjectManager clientObjectManager;
        private bool _isExpanded;
        ServerCommunicationManager serverCommunicationManager;

        public List<IGuiComponent> AllComponents { get; set; }
        public ClientObjectManager ClientObjectManager
        {
            get { return clientObjectManager; }
            set { clientObjectManager = value; }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                }
            }
        }

        public MainWindowViewModel()
        {
            ClientObjectManager = new ClientObjectManager();
            AllComponents = new List<IGuiComponent>
            {
                new Console(),
                new Gauge(),
                new RichConsole(),
                new TimeChartView(),
            };
            ClientObjectManager.ClientObjectCollection = new DispatchingObservableCollection<ClientObject>();
            serverCommunicationManager = new ServerCommunicationManager(clientObjectManager);
            serverCommunicationManager.Start();

        }
    }
}
