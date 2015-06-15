using Components.Gauge;
using Components.Console;
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
using Components;
using Components.RichConsole;

namespace Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridModifier gridModifier;

        Stack<UIElement> elements;

        public ICollectionView ClientIntCollection { get; set; }
        private ClientObjectManager clientObjectManager;
        public ClientObjectManager ClientObjectManager
        {
            get { return clientObjectManager; }
            set { clientObjectManager = value; }
        }

        //public ObservableCollection<IGuiComponent> Components { get; set; }

        public ComponentsList ComponentsList { get; set; }

        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            ComponentsList = ComponentsList.GetInstance();

            //Components = new ObservableCollection<IGuiComponent>();

            elements = new Stack<UIElement>();
            //Components.Add(gauge);
            //ComponentsList.AddComponent(gauge);

            gridModifier = new GridModifier(gridMain);
            //Components.Add(console);
            //ComponentsList.AddComponent(console);

            //Gauge gauge = new Gauge();
            //Components.Add(gauge);
            //gridMain.Children.Add(gauge);
            //Grid.SetColumn(gauge, 0);

            //Components.Console.Console console = new Components.Console.Console();
            //Components.Add(console);
            //gridMain.Children.Add(console);
            //Grid.SetColumn(console, 1);
        }

        private void Button_Toggle_Clicked(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            Sensor clickedSensor = ((Sensor)fe.DataContext);
            clickedSensor.SensorClicked = true;

            ClientObject co = (ClientObject)fe.Tag;

            co.toggleTransmission(clickedSensor.id);
        }

        private void Combo_On_Change(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Sensor cntSensor = comboBox.DataContext as Sensor;

            cntSensor.GuiComponent = comboBox.SelectedItem as IGuiComponent;

            System.Console.WriteLine("asdf");
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = new Components.RichConsole.RichConsole();
            if (gridModifier.AddComponentForCurrentSelection(element))
            {
                elements.Push(element);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                gridModifier.RemoveComponent(elements.Pop());
            }
            catch (InvalidOperationException ex)
            {
                System.Console.WriteLine("No element on the stack");
            }
        }
    }
}
