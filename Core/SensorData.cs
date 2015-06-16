using GuiComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Class represents data sent from Client. It is deserialized from JSON. 
    /// It has sensorDictionary, where key is sensor Id, and singleSensorData object
    /// has the current value of the sensor.
    /// </summary>
    [DataContract]
    class SensorData
    {
        private Dictionary<int, SingleSensorData> sensorsDictionary = new Dictionary<int, SingleSensorData>();

        [DataMember(Name = "sensors")]
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

    [DataContract]
    class SingleSensorData
    {
        [DataMember(Name="value")]
        public string Value { get; set; }

        [DataMember(Name = "timestamp")]
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
