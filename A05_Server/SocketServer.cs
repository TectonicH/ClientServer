


using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace A05_Server
{
    public class SocketServer
    {
        private volatile bool GoServer = true;
        Dictionary<Guid, GameEngine> ClientDB = new Dictionary<Guid, GameEngine>();

        /*METHOD : StartServer
        * DESCRIPTION : Initiates server start and opens listening socket.
        */
        public void StartServer()
        {
            TcpListener listener = null;

            try
            {
                Int32 port = 13000; // Set the TcpListener to this port
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(localAddr, port);

                listener.Start();
                while (GoServer) // Enter the listening loop.
                {
                    TcpClient client = listener.AcceptTcpClient(); // Perform a blocking call to accept requests.
                    ThreadHandler(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                listener.Stop();
            }
        }

        /*METHOD : ThreadHandler
        * DESCRIPTION : Initiates thread and closes them upon their completion
        */
        private void ThreadHandler(TcpClient client)
        {
            Thread clientThread = StartThread(client);
            clientThread.Join();
            client.Close();
        }

        private Thread StartThread(TcpClient client)
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
            Thread clientThread = new Thread(ts);
            clientThread.Start(client);
            return clientThread;
        }

        /*METHOD : Worker
        * DESCRIPTION : Used in thread, handles ferrying of 
        * all request/response messages between clients and server.
        * It is possible to connect several clients at once to play the game
        */
        private void Worker(Object o)
        {
            TcpClient client = (TcpClient)o; // save reference of client to local
            Guid getId = new Guid(); 
            byte[] bytes = new byte[256]; // buffer for communications
            string request;
            string response;
            NetworkStream stream = client.GetStream(); // open stream to read/write 

            int i = stream.Read(bytes, 0, bytes.Length);
            request = Encoding.ASCII.GetString(bytes, 0, i); // process bytes into string
            string guidString = request.Remove(request.IndexOf(":")); // cut non-Guid field out for later

            try
            {
                if (request.StartsWith("NEWGAME:")) // before converting the int
                {
                    // Use received GUID or generate a new one
                    GameEngine game = new GameEngine(); // start game
                    game.ResetGame();

                    getId = Guid.TryParse(guidString, out _) ? Guid.Parse(guidString) : Guid.NewGuid();
                    ClientDB[getId] = game;

                    response = ProcessResponse(ClientDB[getId], getId);

                }
                else if (request.StartsWith("SHUTDOWN:")) // Parse before conversion
                {
                    GoServer = false; // Gonna shut them all down****************************
                    response = "";
                }
                else // Continue as normal
                {
                    getId = Guid.Parse(guidString);
                    request = request.Substring(guidString.Length); // cut GUID

                    request = request.Trim(':');
                    ClientDB[getId].Guess = int.Parse(request);
                    response = ProcessResponse(ClientDB[getId], getId);

                    // Response GO!
                }

                byte[] msg = Encoding.ASCII.GetBytes(response);
                stream.Write(msg, 0, msg.Length);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException: {0}", ane);
            }
            finally
            {
                stream.Close();
            }
        }

        /*METHOD : ProcessResponse
        * DESCRIPTION : Takes game engine object and GUID as parameters and processes response message to client
        */
        private string ProcessResponse(GameEngine game, Guid getID)
        {
            string id = getID.ToString();
            if (game.CheckRange()) // Guess is within range, game in progress, check through conditions and set string according to protocol
            {
                if (game.WinCondition()) 
                { 
                    id += ":win:"; 
                }
                if (game.CheckHi()) 
                {
                    game.Max = game.Guess - 1;
                    id += ":hi:"; 
                }
                if (game.CheckLo()) 
                {
                    game.Min = game.Guess + 1;
                    id += ":lo:"; 
                }
            }
            else
            {
                id += ":new:";
            }

            id += game.RangeString(); // assign range to end of string according to protocol
            return id;
        }
    }
}