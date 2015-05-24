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

namespace Components.TimeChart
{
    public class ViewModel : INotifyPropertyChanged
    {
        public Dictionary<int, ObservableCollection<Model>> Data { get; set; }

        public ViewModel()
        {
            Data = new Dictionary<int, ObservableCollection<Model>>();
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
