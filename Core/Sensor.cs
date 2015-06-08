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

        [DataMember(Name = "dataType")]
        public string type
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
            val.Type = TypeVal;
            GuiComponent.AddValue(val);
        }
    }
}
