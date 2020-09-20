using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using NetCoreServer;
using System.Threading;
using System.Threading.Tasks;

namespace FlappyWorldServer
{
    class ChatSession : TcpSession
    {
        ChatServer chatServer;

        public ChatSession(TcpServer server) : base(server) {
            chatServer = server as ChatServer;
        }

        protected override void OnConnected()
        {
            Console.WriteLine($"Chat TCP session with Id {Id} connected!");

            // Send invite message
            // string message = "Hello from TCP chat! Please send a message or '!' to disconnect the client!";
            // SendAsync(message);
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"Chat TCP session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {                                 
            //var user = ReqUserUpdate.Parser.ParseFrom(buffer, (int)offset, (int)size) as ReqUserUpdate;
            try{
                var req = Google.Protobuf.RequestRPC.Parser.ParseFrom(buffer, (int)offset, (int)size);
                chatServer.engine.OnPacketRecieved(req);
            }catch(Exception e){
                Console.WriteLine("Error when Parsing: "+e);
            }                                      
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP session caught an error with code {error}");
        }
    }

    public class ChatServer : TcpServer
    {
        public MainGameEngine engine;
        public ChatServer(IPAddress address, int port, MainGameEngine _engine) : base(address, port) {
            engine = _engine;
        }

        protected override TcpSession CreateSession() { return new ChatSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat TCP server caught an error with code {error}");
        }
        public void PrintMessage(IMessage message)
        {
            var descriptor = message.Descriptor;
            foreach (var field in descriptor.Fields.InDeclarationOrder())
            {
                Console.WriteLine(
                    "Field {0} ({1}): {2}",
                    field.FieldNumber,
                    field.Name,
                    field.Accessor.GetValue(message));
            }
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            // TCP server port
            int port = 1111;
            if (args.Length > 0)
                port = int.Parse(args[0]);

            Console.WriteLine($"TCP server port: {port}");

            Console.WriteLine();

            // Create a new TCP chat server
            MainGameEngine engine = new MainGameEngine(); 
            engine.server = new ChatServer(IPAddress.Any, port, engine);            
            var server = engine.server;
            
            // Start the server
            Console.Write("Server starting...");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            //Game Engine Start!        
            engine.Start();

            // Perform text input
            for (;;)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Console.Write("Server restarting...");
                    server.Restart();
                    Console.WriteLine("Done!");
                    continue;
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.Multicast(line);
            }

            // Stop the server
            Console.Write("Server stopping...");
            server.Stop();
            Console.WriteLine("Done!");
        }
    }
}