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

namespace Components.Gauge
{
    public class ViewModel : INotifyPropertyChanged
    {

        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; OnPropertyChanged(new PropertyChangedEventArgs("Score")); }
        }

        public ViewModel()
        {
            Score = 500;
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
