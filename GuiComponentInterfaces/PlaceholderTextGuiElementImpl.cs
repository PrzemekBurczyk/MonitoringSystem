using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    class PlaceholderTextGuiElementImpl : IGuiComponent
    {
        public DataType[] GetTypes() { 
            return new DataType[] { DataType.TEXT };
        }

        public void AddValue(DataValue dataValue)
        {
            Console.WriteLine("Written to placeholder TextElement, value : %s, dataSeriesId : %d", dataValue.Value, dataValue.DataSeriesId);
        }
    }
}
