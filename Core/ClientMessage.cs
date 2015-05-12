using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    class ClientMessage
    {
        [DataMember(Name = "sensors")]
        public List<SensorMessage> sensorMessages;
    }

    [DataContract]
    class SensorMessage
    {
        [DataMember]
        public string timestamp;

        [DataMember]
        public string value;
    }
}
