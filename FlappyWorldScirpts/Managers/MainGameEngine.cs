using System.Collections.Generic;
using System.Threading;
using FlappyWorldServer;
using Google.Protobuf;
using System;
using Reactive.Bindings;
using System.Reactive.Linq;

public class MainGameEngine
{   
    #region Managers
    //contains Managers
    public UserMnanger userManager;
    public CollisionManager collisionManager;
    public RPCManager rpcManager;

    public HashSet<Actor> allActors = new HashSet<Actor>();

    public float deltaTime {get; private set;}

    #endregion
    

    public void SetUpManagers()
    {
        userManager = new UserMnanger(this);
        collisionManager = new CollisionManager(this);
        rpcManager = new RPCManager(this);
    }


    public ReactiveProperty<float> UpdateLogic = new ReactiveProperty<float>();
    UsersTest usersTest;
    public ChatServer server;

    public void Start()
    {
        Thread t1 = new Thread(new ThreadStart(MainLoop));
        SetUpManagers();
        usersTest = new UsersTest(this); 
        t1.Start();  

    }

    void MainLoop()
    {
        deltaTime = 0.1f;
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
                //Console.WriteLine("server is null");
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