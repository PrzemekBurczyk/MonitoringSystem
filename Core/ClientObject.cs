using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    public class ClientObject
    {
        [DataMember(Name="deviceName")]
        public String Name { get; set; }

        public ClientObject(String name)
        {
            Name = name;
        }
        
        public ClientConnection clientConnection;

        [DataMember(Name = "sensors")] 
        private List<Sensor> sensorsList = new List<Sensor>();
        public List<Sensor> sensors
        {
            get
            {
                return sensorsList;
            }
            set
            {
                sensorsList = value;
            }
        }

        public void passData(String data)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
                settings.UseSimpleDictionaryFormat = true;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SensorData), settings);
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(data));
            SensorData sensorData = (SensorData)serializer.ReadObject(ms);

            foreach (Sensor sensor in sensors)
            {
                try
                {
                    SingleSensorData singleSensorData = sensorData.SensorsDictionary[sensor.id];
                    if (singleSensorData != null)
                    {
                        sensor.AddValue(singleSensorData.getDataValue());
                    }
                }
                catch (KeyNotFoundException e)
                {
                    continue;
                }
                
            }
        }

        public void toggleTransmission(int sensorId)
        {
            clientConnection.WriteMessage("{\"sensorsToUpdate\":{\""  + sensorId.ToString() + "\":true}}");
        }

        public void sendDataBack()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClientObject));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, this);

            StreamReader sr = new StreamReader(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string data = sr.ReadToEnd();
            clientConnection.WriteMessage(data);
        }
    }
}
