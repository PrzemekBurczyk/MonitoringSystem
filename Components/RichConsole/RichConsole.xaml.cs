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

namespace Components.RichConsole
{
    /// <summary>
    /// Interaction logic for RichConsole.xaml
    /// </summary>
    public partial class RichConsole : UserControl, IGuiComponent, INotifyPropertyChanged
    {
        public void addData(string line, int seriesId)
        {
            Run myRun = new Run(line);
            Paragraph myParagraph = new Paragraph();
            myParagraph.Inlines.Add(myRun);
            myParagraph.Foreground = getColor(seriesId);
            myParagraph.LineHeight = 1;
            rtb.Document.Blocks.Add(myParagraph);
            rtb.ScrollToEnd();
        }

        public void addData(string[] lines, int seriesId)
        {
            foreach (string line in lines)
            {
                addData(line, seriesId);
            }
        }

        public RichConsole()
        {
            InitializeComponent();
            State = false;
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
        public DataType[] GetTypes()
        {
            return new DataType[] { DataType.TEXT, DataType.INTEGER };
        }

        public string ComponentDisplayName
        {
            get { return "RichConsole"; }
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

        public void AddValue(DataValue dataValue)
        {
            addData(new DateTime(dataValue.Timestamp).ToLongTimeString() + " - " + dataValue.Value, dataValue.DataSeriesId);
        }

        private SolidColorBrush getColor(int seriesId)
        {
            if (seriesId % 5 == 0)
            {
                return Brushes.Black;
            }
            else if (seriesId % 5 == 1)
            {
                return Brushes.Blue;
            } 
            else if (seriesId % 5 == 2)
            {
                return Brushes.Green;
            }
            else if (seriesId % 5 == 3)
            {
                return Brushes.Yellow;
            }
            else
            {
                return Brushes.Red;
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
            return new RichConsole();
        }
    }
}
