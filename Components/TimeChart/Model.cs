using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.TimeChart
{
    public class Model
    {
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public DataType Type { get; set; }
        public int DataSeriesId { get; set; }
    }
}
