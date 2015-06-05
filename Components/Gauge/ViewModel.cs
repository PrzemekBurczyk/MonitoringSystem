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
        public event PropertyChangedEventHandler PropertyChanged;
        public EventHandler OnScoreChanged;

        private int score;
        public int Score
        {
            get { return score; }
            set {
                if (value != score)
                {
                    score = value;
                    OnPropertyChanged("Score");
                    if (OnScoreChanged != null)
                    {
                        OnScoreChanged(this, EventArgs.Empty);
                    }
                }
     
            }
        }

        public ViewModel()
        {
            Score = 0;
        }

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }

    }
}
