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
    public class Sensor
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String name { get; set; }

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

        public IGuiComponent GuiComponent { get; set; }
        public int DataSeriesId { get; set; }

        public void AddValue(DataValue val)
        {
            if (GuiComponent == null)
            {
                Console.WriteLine("Passing data : " + val.Value.ToString() + ", but component is null!!!");
                return;
            }

            val.DataSeriesId = DataSeriesId;
            GuiComponent.AddValue(val);
        }
    }
}
