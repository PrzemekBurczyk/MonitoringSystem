using GuiComponentInterfaces;
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
using System.Windows.Threading;

namespace Components.TimeChart
{
    /// <summary>
    /// Interaction logic for TimeChartView.xaml
    /// </summary>
    public partial class TimeChartView : UserControl, IGuiComponent
    {
        private ViewModel ViewModel { get; set; }

        private DispatcherTimer timer;
        private long time;

        public TimeChartView()
        {
            InitializeComponent();

            Loaded += TimeChartView_Loaded;

            RunDemo();
        }

        private void RunDemo()
        {
            timer = new DispatcherTimer();
            time = DateTime.Now.Ticks;

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now.Ticks;

            Random random = new Random();

            DataValue dataValue = new DataValue();
            dataValue.DataSeriesId = 1;
            dataValue.Timestamp = time;
            dataValue.Type = DataType.INTEGER;
            dataValue.Value = (random.NextDouble() * 100).ToString();

            AddValue(dataValue);
        }

        void TimeChartView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ViewModel) MainGrid.DataContext;
        }

        public DataType[] GetTypes()
        {
            return new DataType[] { DataType.INTEGER };
        }

        public void AddValue(DataValue dataValue)
        {
            if(GetTypes().Contains(dataValue.Type)){
                if (dataValue.Type == DataType.INTEGER)
                {
                    Model model = new Model();
                    model.DataSeriesId = dataValue.DataSeriesId;
                    model.Time = new DateTime(dataValue.Timestamp);
                    model.Value = Double.Parse(dataValue.Value);

                    if (ViewModel.Data.Count >= 60)
                    {
                        ViewModel.Data.RemoveAt(0);
                    }
                    ViewModel.Data.Add(model);
                }
            }
        }
    }
}
