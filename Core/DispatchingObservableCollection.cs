using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Core
{
    public class DispatchingObservableCollection<T> : ObservableCollection<T>
    {
        private readonly Dispatcher currentDispatcher;
        public DispatchingObservableCollection()
        {
            //Assign the current Dispatcher (owner of the collection)
            currentDispatcher = Dispatcher.CurrentDispatcher;
        }

        protected override void ClearItems()
        {
            DoDispatchedAction(BaseClearItems);
        }

        private void BaseClearItems()
        {
            base.ClearItems();
        }
        protected override void InsertItem(int index, T item)
        {
            DoDispatchedAction(() => BaseInsertItem(index, item));
        }

        private void BaseInsertItem(int index, T item)
        {
            base.InsertItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            DoDispatchedAction(() => BaseMoveItem(oldIndex, newIndex));
        }

        private void BaseMoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            DoDispatchedAction(() => BaseRemoveItem(index));
        }

        private void BaseRemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            DoDispatchedAction(() => BaseSetItem(index, item));
        }

        private void BaseSetItem(int index, T item)
        {
            base.SetItem(index, item);
        }

        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DoDispatchedAction(() => BaseOnCollectionChanged(e));
        }

        private void BaseOnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            DoDispatchedAction(() => BaseOnPropertyChanged(e));
        }

        private void BaseOnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        private void DoDispatchedAction(Action action)
        {
            if (currentDispatcher.CheckAccess())
            {
                action.Invoke();
            }
            else
            {
                currentDispatcher.Invoke(DispatcherPriority.DataBind, action);
            }
        } 
    }
}
