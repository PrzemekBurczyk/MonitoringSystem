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
using System.Collections.ObjectModel;

namespace Components.PieChart
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : UserControl
    {
        private readonly ObservableCollection<Population> _populations = new ObservableCollection<Population>();
        public ObservableCollection<Population> Populations
        {
            get
            {
                return _populations;
            }
        }

        public PieChart()
        {
            InitializeComponent();
            _populations.Add(new Population() { Name = "China", Count = 1340 });
            _populations.Add(new Population() { Name = "India", Count = 1220 });
            _populations.Add(new Population() { Name = "United States", Count = 309 });
            _populations.Add(new Population() { Name = "Indonesia", Count = 240 });
            _populations.Add(new Population() { Name = "Brazil", Count = 195 });
            _populations.Add(new Population() { Name = "Pakistan", Count = 174 });
            _populations.Add(new Population() { Name = "Nigeria", Count = 158 });
        }
    }
}
