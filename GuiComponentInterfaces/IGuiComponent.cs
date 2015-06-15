using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    public interface IGuiComponent
    {
        String ComponentDisplayName { get; set; }
        bool State { get; set; }
        void AddValue(DataValue dataValue);

        IGuiComponent getNewInstance();
    }
}
