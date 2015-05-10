using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClientObject
    {
        public String Name { get; set; }
        public String type;

        public ClientObject(String name)
        {
            Name = name;
        }
        
        public ClientConnection clientConnection;

        public void passData(String data)
        {
            Console.WriteLine("Passing data to sensors : " + data);
        }

        public void toggleTransmission(int sensorId)
        {
            clientConnection.WriteMessage("Transmission toggled for sensor "  + sensorId.ToString() + "\n");
        }
    }
}
