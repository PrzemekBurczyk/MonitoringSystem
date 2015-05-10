using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class ClientObject
    {
        public String type;
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
