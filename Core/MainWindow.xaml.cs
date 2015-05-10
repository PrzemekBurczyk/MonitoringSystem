using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerCommunicationManager serverCommunicationManager;
        ClientObjectManager clientObjectManager;

        public MainWindow()
        {
            InitializeComponent();

            clientObjectManager = new ClientObjectManager();
            serverCommunicationManager = new ServerCommunicationManager(clientObjectManager);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serverCommunicationManager.Start();
            ToggleButton.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Run this only after connection and receiving initial data from Client
            ClientObject placeholder = clientObjectManager.getObject(0);
            if (placeholder != null)
            {
                placeholder.toggleTransmission(1);
            }
        }
    }
}
