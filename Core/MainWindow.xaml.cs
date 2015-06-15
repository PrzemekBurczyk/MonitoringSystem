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

        public ObservableCollection<IGuiComponent> ElementsCollection { get; set; }

        private IGuiComponent CurrentlyChosenComponent { get; set; }

        private ClientObjectManager clientObjectManager;
        public ClientObjectManager ClientObjectManager
        {
            get { return clientObjectManager; }
            set { clientObjectManager = value; }
        }

        public ObservableCollection<ClientObject> ClientObjectCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            ElementsCollection = new ObservableCollection<IGuiComponent>();

            gridModifier = new GridModifier(gridMain);

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
        }

        private void Add_Component_Button_Click(object sender, RoutedEventArgs e)
        {
            IGuiComponent element = CurrentlyChosenComponent.getNewInstance();
            if (gridModifier.AddComponentForCurrentSelection( (UIElement) element))
            {
                ElementsCollection.Add(element);
            }
        }

        private void Remove_Component_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IGuiComponent removedElement = ElementsCollection.Last<IGuiComponent>();
                ElementsCollection.Remove(removedElement);
                gridModifier.RemoveComponent((UIElement)removedElement);
            }
            catch (InvalidOperationException ex)
            {
                System.Console.WriteLine("No element on the stack");
            }
        }

        private void DataGrid_OnMouseDoublClick(object sender, MouseButtonEventArgs e)
        {
            var frameworkElement = (FrameworkElement)sender;

            var component = frameworkElement.DataContext as IGuiComponent;

            if (component.State == false)
            {
                component.State = true;
                CurrentlyChosenComponent = component;
            }
            else
            {
                component.State = false;
                CurrentlyChosenComponent = null;
            }
        }
    }
}
