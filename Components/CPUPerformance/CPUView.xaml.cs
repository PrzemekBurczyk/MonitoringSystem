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

namespace Components.CPUPerformance
{
    /// <summary>
    /// Interaction logic for CPUView.xaml
    /// </summary>
    public partial class CPUView : UserControl
    {
        public CPUView()
        {
            InitializeComponent();
            ViewModel viewModel = (ViewModel)MainGrid.DataContext;
            viewModel.OnTick += new EventHandler(OnTick);
        }

        private void OnTick(object sender, EventArgs eventArgs) {
            YAxis.MaxValue = int.Parse(YAxis.MaxValue.ToString()) + 1;
            
        }
    }
}
