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

        public List<SensorObject> sensors;

        public void passData(String data)
        {
            Console.WriteLine("Passing data to sensors : " + data);
        }

        public void toggleTransmission(int sensorId)
        {
            //clientConnection.toggleTransmission
        }
    }
}
