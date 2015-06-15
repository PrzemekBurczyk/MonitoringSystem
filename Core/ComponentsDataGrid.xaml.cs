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
using GuiComponentInterfaces;

namespace Core
{
    /// <summary>
    /// Interaction logic for ComponentsDataGrid.xaml
    /// </summary>
    public partial class ComponentsDataGrid : UserControl
    {
        public ComponentsDataGrid()
        {
            InitializeComponent();
        }

        private void DataGrid_OnMouseDoublClick(object sender, MouseButtonEventArgs e)
        {
            var frameworkElement = (FrameworkElement) sender;
            var component = frameworkElement.DataContext as IGuiComponent;
            var componentsList = ComponentsList.GetInstance();
            if (component.State == false)
            {
                component.State = true;
                componentsList.AddComponent(component);
            }
            else
            {
                component.State = false;
            }
        }
    }
}
