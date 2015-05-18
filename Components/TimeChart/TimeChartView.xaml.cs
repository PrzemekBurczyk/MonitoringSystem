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

namespace Components.TimeChart
{
    /// <summary>
    /// Interaction logic for TimeChartView.xaml
    /// </summary>
    public partial class TimeChartView : UserControl, IGuiComponent
    {
        public TimeChartView()
        {
            InitializeComponent();
            ViewModel viewModel = (ViewModel)MainGrid.DataContext;
            viewModel.OnTick += new EventHandler(OnTick);
        }

        private void OnTick(object sender, EventArgs eventArgs)
        {
            //YAxis.MaxValue = int.Parse(YAxis.MaxValue.ToString()) + 1;
        }

        public override DataType[] GetTypes()
        {
            return new DataType[] { DataType.INTEGER };
        }

        public override void AddValue(DataValue dataValue)
        {

        }
    }
}
