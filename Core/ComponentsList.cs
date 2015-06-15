using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Components.Annotations;
using GuiComponentInterfaces;

namespace Core
{
    public class ComponentsList : INotifyPropertyChanged
    {
        public ObservableCollection<IGuiComponent> Components
        {
            get; set;
        }

        private static ComponentsList _instance;

        private ComponentsList()
        {
            Components = new ObservableCollection<IGuiComponent>();
        }

        public static ComponentsList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ComponentsList();
            }
            return _instance;
        }

        public void AddComponent(IGuiComponent component)
        {
            Components.Add(component);
            OnPropertyChanged("Components");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
