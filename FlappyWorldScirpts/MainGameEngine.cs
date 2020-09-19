using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using FlappyWorldServer;
using Google.Protobuf;
using Reactive.Bindings;

public class MainGameEngine
{   
    //contains Managers
    public UserMnanger userManager;
    public CollisionManager collisionManager;

    public void SetUpManagers()
    {
        userManager = new UserMnanger(this);
        collisionManager = new CollisionManager(this);
    }


    public ReactiveProperty<float> UpdateLogic = new ReactiveProperty<float>();
    public static float deltaTime = 0.1f;
    UsersTest usersTest;
    public ChatServer server;

    public void Start()
    {
        Thread t1 = new Thread(new ThreadStart(MainLoop));        
        usersTest = new UsersTest();        
        t1.Start();
        //test player
    }

    void MainLoop()
    {
        while(true)
        {
            //initialized
            //start
        
            //update
            UpdateLoop();

            //late update
            Thread.Sleep(100);
            
            if(server == null)
            {
                Console.WriteLine("server is null");
                continue;
                
            }
                            

            //RspUserUpdate rsp = new RspUserUpdate();
			//rsp.Users.AddRange(UserMnanger..dictUsers.Values);    
            //server send user updated
            //server.Multicast(rsp.ToByteArray());
        }        
    }

    internal void OnPacketRecieved(RequestRPC req)
    {
        Console.WriteLine("req : "+req);
    }

    void UpdateLoop()
    {
        UpdateLogic.ForceNotify();
    }
}