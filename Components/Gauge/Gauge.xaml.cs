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

namespace Components.Gauge
{
    /// <summary>
    /// Interaction logic for Gauge.xaml
    /// </summary>
    public partial class Gauge : UserControl, IGuiComponent
    {

        private ViewModel ViewModel { get; set; }

        public Gauge()
        {
            InitializeComponent();
            ViewModel = (ViewModel)MainGrid.DataContext;
        }

        public DataType[] GetTypes()
        {
            return new DataType[] { DataType.INTEGER };
        }

        public void AddValue(DataValue dataValue)
        {
            if (dataValue.Type == DataType.INTEGER)
            {
                ViewModel.Score = Int32.Parse(dataValue.Value);                
            }
        }
    }
}
