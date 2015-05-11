using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Components.Console
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public EventHandler OnConsoleTextChanged;

        private string consoleText;
        public string ConsoleText
        {
            get { return consoleText; }
            set
            {
                if (value != consoleText)
                {
                    consoleText = value;
                    OnPropertyChanged("ConsoleText");
                    if (OnConsoleTextChanged != null)
                    {
                        OnConsoleTextChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }

        public ViewModel()
        {
            ConsoleText = "";
        }
    }
}
