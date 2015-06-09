using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    public interface IGuiComponent
    {
        string DataTypesStr
        {
            get; set; 
        }

        DataType[] GetTypes();

        void AddValue(DataValue dataValue);

        String typesToString();
    }
}
