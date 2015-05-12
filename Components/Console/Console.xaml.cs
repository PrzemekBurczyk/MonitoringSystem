using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
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

namespace Components.Console
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl, IGuiComponent
    {
        private ViewModel ViewModel { get; set; }

        public void addData(string line)
        {
            ViewModel.ConsoleText += "\n" + line;
        }

        public void addData(string[] lines)
        {
            foreach (string line in lines) {
                addData(line);
            }
        }

        public void clearData()
        {
            ViewModel.ConsoleText = "";
        }

        public Console()
        {
            InitializeComponent();
            Loaded += Console_Loaded;
        }

        private void Console_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ViewModel)MainGrid.DataContext;
            ViewModel.OnConsoleTextChanged += new EventHandler(OnConsoleTextChanged);
        }

        private void OnConsoleTextChanged(object sender, EventArgs eventArgs)
        {
            textBox.ScrollToEnd();
        }

        public DataType[] GetTypes()
        {
            return new DataType[] { DataType.TEXT, DataType.VECTOR };
        }

        public void AddValue(DataValue dataValue)
        {
            addData(dataValue.Value);
        }
    }
}
