using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    class SensorData
    {
        [DataMember(Name = "sensors")]
        private Dictionary<int, SingleSensorData> sensorsDictionary = new Dictionary<int, SingleSensorData>();
        public Dictionary<int, SingleSensorData> SensorsDictionary
        {
            get
            {
                return sensorsDictionary;
            }
            set
            {
                sensorsDictionary = value;
            }
        }
    }

    class SingleSensorData
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Timestamp { get; set; }

        public DataValue getDataValue()
        {
            DataValue val = new DataValue();
            val.Timestamp = long.Parse(Timestamp);
            val.Value = Value;

            return val;
        }
    }
}
