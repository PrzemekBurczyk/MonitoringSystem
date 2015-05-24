﻿using GuiComponentInterfaces;
using Sparrow.Chart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private int currentSeries = 0;

        public TimeChartView()
        {
            InitializeComponent();

            Loaded += TimeChartView_Loaded;
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
            dataValue.DataSeriesId = random.Next(7);
            dataValue.Timestamp = time;
            dataValue.Type = DataType.INTEGER;
            dataValue.Value = (random.NextDouble() * 100).ToString();

            AddValue(dataValue);
        }

        void TimeChartView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ViewModel) MainGrid.DataContext;
            //RunDemo();
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

                    if (!ViewModel.Data.ContainsKey(model.DataSeriesId))
                    {
                        ViewModel.Data.Add(model.DataSeriesId, new ObservableCollection<Model>());
                        try
                        {
                            ((AreaSeries)Chart.Series[currentSeries++]).PointsSource = ViewModel.Data[model.DataSeriesId];
                        }
                        catch (Exception e)
                        {
                            System.Console.WriteLine("Cannot add another series");
                        }
                        //AddSeries(model.DataSeriesId);
                    }

                    ObservableCollection<Model> data = ViewModel.Data[model.DataSeriesId];

                    if (data.Count >= 60)
                    {
                        data.RemoveAt(0);
                    }

                    data.Add(model);

                    foreach(ObservableCollection<Model> collection in ViewModel.Data.Values){
                        if (collection != data)
                        {
                            if (collection.Count >= 60)
                            {
                                collection.RemoveAt(0);
                            }

                            Model last = collection.Last();
                            last.Time = model.Time;
                            collection.Add(last);
                        }
                    }
                }
            }
        }

        private void AddSeries(int id)
        {
            AreaSeries areaSeries = new AreaSeries();
            areaSeries.PointsSource = ViewModel.Data[id];
            areaSeries.XPath = "Time";
            areaSeries.YPath = "Value";
            areaSeries.Stroke = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF117DBB"));
            areaSeries.Fill = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#11117DBB"));
            areaSeries.StrokeThickness = 1.0;
            Chart.Series.Add(areaSeries);
        }
    }
}
