using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    class PlaceholderTextGuiElementImpl : IGuiComponent
    {
        public string DataTypesStr { get; set; }

        public DataType[] GetTypes() { 
            return new DataType[] { DataType.TEXT };
        }

        public void AddValue(DataValue dataValue)
        {
            Console.WriteLine("Written to placeholder TextElement, value : %s, dataSeriesId : %d", dataValue.Value, dataValue.DataSeriesId);
        }
        public string typesToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataType dt in GetTypes())
            {
                builder.Append(dt.ToString());
            }
            DataTypesStr = builder.ToString();
            return builder.ToString();
        }
    }
}
