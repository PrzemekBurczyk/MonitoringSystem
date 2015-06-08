using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GuiComponentInterfaces;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class Sensor
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String name { get; set; }


        [DataMember]
        public string description;

        private DataType _typeVal;

        public DataType TypeVal
        {
            get { return _typeVal; }
            set { _typeVal = value; }
        }
        [DataMember]
        string type
        {
            get
            {
                return _typeVal.ToString();
            }
            set
            {
                this._typeVal = (DataType)Enum.Parse(typeof(DataType), value, true);
            }
        }

        IGuiComponent guiComponent;
    }
}
