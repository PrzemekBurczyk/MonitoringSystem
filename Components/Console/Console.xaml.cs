using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Components.Annotations;

namespace Components.Console
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl, IGuiComponent, INotifyPropertyChanged
    {
        private ViewModel ViewModel { get; set; }

        public void addData(string line)
        {
            ViewModel.ConsoleText += line + "\n";
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
            //Button btn = new Button();
            //btn.Name = "btn1";
            //btn.Click += btn1_Click;
        }
                   
        //private void btn1_Click(object sender, RoutedEventArgs e)
        //{
        //    DataValue dv = new DataValue();
        //    dv.Timestamp = System.DateTime.Now.Ticks;
        //    dv.Value = "123";
        //    dv.Type = DataType.TEXT;
        //    dv.DataSeriesId = 1;
        //    AddValue(dv);
        //}


        private void Console_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ViewModel)MainGrid.DataContext;
            ViewModel.OnConsoleTextChanged += new EventHandler(OnConsoleTextChanged);
            State = false;
        }

        private void OnConsoleTextChanged(object sender, EventArgs eventArgs)
        {
            Dispatcher.Invoke((Action)(() => { textBox.ScrollToEnd(); }));
        }

        public string DataTypesStr { get; set; }

        public string ComponentDisplayName
        {
            get
            {
                return "Console";
            }
            set {}
        }

        private bool _state;

        public bool State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public DataType[] GetTypes()
        {
            return new DataType[] { DataType.TEXT, DataType.INTEGER };
        }

        public void AddValue(DataValue dataValue)
        {
            addData(new DateTime(dataValue.Timestamp).ToLongTimeString() + " - " + dataValue.Value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public IGuiComponent getNewInstance()
        {
            return new Console();
        }
    }
}
