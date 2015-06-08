﻿using Components.Gauge;
using Components.RichConsole;
using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ICollectionView ClientIntCollection { get; set; }
        private ClientObjectManager clientObjectManager;
        public ClientObjectManager ClientObjectManager
        {
            get { return clientObjectManager; }
            set { clientObjectManager = value; }
        }

        public List<IGuiComponent> components = new List<IGuiComponent>();

        //public ICollectionView ClientObjectCollection { get; set; }
        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            //clientObjectManager = new ClientObjectManager();
            //ClientObjectCollection = new ObservableCollection<ClientObject> { new ClientObject("Pepper"), new ClientObject("Zoe") };

            //serverCommunicationManager = new ServerCommunicationManager(clientObjectManager);

            //automatyczne nasłuchiwanie przy starcie aplikacji
            //serverCommunicationManager.Start();
            //ToggleButton.IsEnabled = true;

            Gauge gauge = new Gauge();
            components.Add(gauge);        
            gridMain.Children.Add(gauge);

            //RichConsole console = new RichConsole();
            //components.Add(console);

            //gridMain.Children.Add(console);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //serverCommunicationManager.Start();
            //ToggleButton.IsEnabled = true;
        }

        private void Button_Toggle_Clicked(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            Sensor clickedSensor = ((Sensor)fe.DataContext);

            clickedSensor.GuiComponent = components[0];

            ClientObject co = (ClientObject)fe.Tag;

            co.toggleTransmission(clickedSensor.id);
        } 
    }
}
