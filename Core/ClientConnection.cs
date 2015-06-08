using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ClientConnection
    {
        public TcpClient tcpClient;
        protected NetworkStream clientStream;

        public ClientDataReceivedDelegate onDataReceived;

        public byte[] receivedDataLeftovers;

        public ClientConnection(TcpClient client)
        {
            tcpClient = client;
            clientStream = tcpClient.GetStream();
        }

        public ClientObject retreiveClientObject()
        {
            Console.WriteLine("Waiting for data from client");

            string clientJsonData = readMessage();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ClientObject));
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(clientJsonData));
            ClientObject clientData = (ClientObject)serializer.ReadObject(ms);

            return clientData;
        }

        public void HandleClientComm()
        {
            try
            {
                while (true)
                {
                    String message = readMessage();
                    onDataReceived(message);
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
            byte[] message = new byte[4096], tmpMessage = new byte[4096], msgLengthBuff = new byte[4];
            int bytesRead = 0, msgLength=0;

            bytesRead = clientStream.Read(message, 0, 4096);

            if (bytesRead == 0)
            {
                //the client has disconnected from the server
                throw new IOException();
            }

            if (receivedDataLeftovers != null && receivedDataLeftovers.Length > 0)
            {
                byte[] messageWithPreviousReceived = new byte[bytesRead + receivedDataLeftovers.Length];
                Array.Copy(receivedDataLeftovers, messageWithPreviousReceived, receivedDataLeftovers.Length);
                Array.Copy(message, 0, messageWithPreviousReceived, receivedDataLeftovers.Length, bytesRead);
                message = messageWithPreviousReceived;
            }
            

            msgLengthBuff = message.Take<byte>(4).ToArray();
            message = message.Skip<byte>(4).ToArray();
            bytesRead -= 4;

            if (BitConverter.IsLittleEndian)
                Array.Reverse(msgLengthBuff);

            msgLength = BitConverter.ToInt32(msgLengthBuff, 0);

            while( bytesRead < msgLength)
            {
                int tmpBytesRead = clientStream.Read(tmpMessage, 0, 4096);
                Array.Copy(tmpMessage, 0, message, bytesRead, tmpBytesRead);
                bytesRead += tmpBytesRead;
            }

            if (bytesRead > msgLength)
            {
                receivedDataLeftovers = new byte[bytesRead - msgLength];
                Array.Copy(message, msgLength, receivedDataLeftovers, 0, bytesRead - msgLength);

                message = message.Take<byte>(msgLength).ToArray();
            }

            //message has successfully been received
            ASCIIEncoding encoder = new ASCIIEncoding();
            string resultMsg = encoder.GetString(message, 0, msgLength);
            return resultMsg;
        }

        public void WriteMessage(String message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] responseMessage = encoder.GetBytes(message);
            int length = responseMessage.Length;
            byte[] msgLengthBuffer = BitConverter.GetBytes(length);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(msgLengthBuffer);

            byte[] concatenatedBuff = msgLengthBuffer.Concat(responseMessage).ToArray();

            clientStream.Write(concatenatedBuff, 0, concatenatedBuff.Length);            
        }
    } 
}
