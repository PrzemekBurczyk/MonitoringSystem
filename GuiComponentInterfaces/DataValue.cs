using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    public class DataValue
    {
        public long Timestamp { get; set; }
        public string Value { get; set;}
        public DataType Type { get; set;}
        public int DataSeriesId { get; set;}
    }
}
