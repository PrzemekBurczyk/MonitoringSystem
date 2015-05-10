using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClientConnection
    {
        public TcpClient tcpClient;
        protected NetworkStream clientStream;

        public ClientDataReceivedDelegate onDataReceived;

        public void HandleClientComm()
        {
            clientStream = tcpClient.GetStream();

            try
            {
                String sensorData = readMessage();
                WriteMessage("OK");

                while (true)
                {
                    String message = readMessage();
                    onDataReceived(message);
                    //Console.WriteLine("Message : " + message);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("User disconnected");
            }
            catch (SocketException)
            {
                Console.WriteLine("Socket error occured");
            }

            tcpClient.Close();
        }

        public string readMessage()
        {
            byte[] message = new byte[4096];
            int bytesRead;

            bytesRead = 0;


            bytesRead = clientStream.Read(message, 0, 4096);

            if (bytesRead == 0)
            {
                //the client has disconnected from the server
                throw new IOException();
            }

            //message has successfully been received
            ASCIIEncoding encoder = new ASCIIEncoding();
            return encoder.GetString(message, 0, bytesRead);
        }

        public void WriteMessage(String message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] responseMessage = encoder.GetBytes(message);
            clientStream.Write(responseMessage, 0, responseMessage.Length);
        }
    } 
}
