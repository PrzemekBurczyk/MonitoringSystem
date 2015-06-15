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

        public string ComponentDisplayName { get; set; }
        public bool State { get; set; }

        public void AddValue(DataValue dataValue)
        {
            Console.WriteLine("Written to placeholder TextElement, value : %s, dataSeriesId : %d", dataValue.Value, dataValue.DataSeriesId);
        }

        public IGuiComponent getNewInstance()
        {
            return new PlaceholderTextGuiElementImpl();
        }
    }
}
