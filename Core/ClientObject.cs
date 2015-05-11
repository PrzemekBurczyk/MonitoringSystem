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
        List<Sensor> sensors = new List<Sensor>();

        public void passData(String data)
        {


            Console.WriteLine("Passing data to sensors : " + data);
        }

        public void toggleTransmission(int sensorId)
        {
            clientConnection.WriteMessage("Transmission toggled for sensor "  + sensorId.ToString() + "\n");
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
