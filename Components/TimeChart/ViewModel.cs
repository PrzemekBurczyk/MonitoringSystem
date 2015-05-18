using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Threading;

namespace Components.TimeChart
{
    public class ViewModel : INotifyPropertyChanged
    {
        public EventHandler OnTick { get; set; }

        public ObservableCollection<Model> Data { get; set; }

        private DispatcherTimer timer;
        private DateTime time;

        public ViewModel()
        {
            Data = new ObservableCollection<Model>();
            timer = new DispatcherTimer();
            time = DateTime.Now;

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += timer_Tick;
            timer.Start();
            for (int i = 0; i < 60; i++)
            {
                Model model = new Model();
                model.Time = time;
                model.Value = 0.0;

                Data.Add(model);
                time = time.AddSeconds(1);
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            Data.RemoveAt(0);

            Model model = new Model();
            model.Time = time;
            model.Value = 0.0;

            Data.Add(model);
            time = time.AddSeconds(1);

            if (OnTick != null)
            {
                OnTick(this, EventArgs.Empty);
            }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
