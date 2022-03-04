
using System;
using System.Net.Sockets;

namespace A05_Client
{
    public class Communication 
    {
        /*
       * METHOD : ConnectClient
       * DESCRIPTION : This method will connect to the server and begin the program.
       * TITLE : “TcpClient Class"
       * DATE : 2021-11-12
       * AVAILABIILTY : https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=net-5.0
       */
        public static string ConnectClient(String server, int port, String message)
        {
            // String to store the response ASCII representation.
            string responseData = string.Empty;
            TcpClient client = null;
            NetworkStream stream = null;

            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Close everything.
                stream.Close();
                client.Close();
            }
            return responseData;
        }
    } 
}
