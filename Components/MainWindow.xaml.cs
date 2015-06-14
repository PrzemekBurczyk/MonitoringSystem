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

namespace Components
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridModifier gridModifier;

        Stack<UIElement> elements;

        public MainWindow()
        {
            InitializeComponent();

            elements = new Stack<UIElement>();

            gridModifier = new GridModifier(MainGrid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = new RichConsole.RichConsole();
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
