



namespace A05_Server
{
     class Program
    {
        private static void Main(string[] args)
        {
            SocketServer server = new SocketServer();

            server.StartServer(); // Server START!
        }
    }
}
