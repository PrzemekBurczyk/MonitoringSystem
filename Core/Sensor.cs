using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GuiComponentInterfaces;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    class Sensor
    {
        [DataMember]
        int id;

        [DataMember]
        String name;

        [DataMember]
        public string description;

        private DataType typeVal;
        [DataMember]
        string type
        {
            get
            {
                return typeVal.ToString();
            }
            set
            {
                this.typeVal = (DataType)Enum.Parse(typeof(DataType), value, true);
            }
        }

        IGuiComponent guiComponent;
    }
}
