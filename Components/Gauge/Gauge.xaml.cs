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

namespace Components.Gauge
{
    /// <summary>
    /// Interaction logic for Gauge.xaml
    /// </summary>
    public partial class Gauge : UserControl, IGuiComponent, INotifyPropertyChanged
    {

        private ViewModel ViewModel { get; set; }

        public Gauge()
        {
            InitializeComponent();
            ViewModel = (ViewModel)MainGrid.DataContext;
            State = false;
        }
        public string ComponentDisplayName
        {
            get
            {
                return "Gauge";
            }
            set { }
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
            return new DataType[] { DataType.INTEGER };
        }

        public void AddValue(DataValue dataValue)
        {
            if (dataValue.Type == DataType.INTEGER)
            {
                ViewModel.Score = Int32.Parse(dataValue.Value);                
            }
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
            return new Gauge();
        }
    }
}
