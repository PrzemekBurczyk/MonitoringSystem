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
    /// <summary>
    /// Class representing Client - the one which connects to our app via TCP ( usually a robot).
    /// </summary>
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

        /// <summary>
        /// Passes data received from client to sensors.
        /// </summary>
        /// <param name="data"></param>
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

        /// <summary>
        /// Sends message to Client to turn off or on sending of sensor data from client.
        /// </summary>
        /// <param name="sensor"></param>
        public void toggleTransmission(Sensor sensor)
        {
            sensor.Toggle();
            clientConnection.WriteMessage("{\"sensorsToUpdate\":{\""  + sensor.id.ToString() + "\":" + sensor.SensorClicked + "}}");
        }
    }
}
