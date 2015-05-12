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

        public void AddData(string line)
        {
            ViewModel.ConsoleText += line + "\n";
        }

        public void AddData(string[] lines)
        {
            foreach (string line in lines) {
                AddData(line);
            }
        }

        public void ClearData()
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
            return new DataType[] { DataType.TEXT };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataValue dataValue = new DataValue();
            dataValue.Timestamp = DateTime.Now.Ticks;
            dataValue.Value = "new line";
            dataValue.DataSeriesId = 0;
            dataValue.Type = DataType.TEXT;
            AddValue(dataValue);
        }

        public void AddValue(DataValue dataValue)
        {
            AddData(new DateTime(dataValue.Timestamp).ToString() + " > " + dataValue.Value);
        }
    }
}
