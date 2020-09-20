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
        userManager = ActorMaker<UserMnanger>.New(this);
        collisionManager = ActorMaker<CollisionManager>.New(this);
        rpcManager = ActorMaker<RPCManager>.New(this);
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
            
            //late update
            Thread.Sleep(100);
            
            if(server == null)
            {
                //Console.WriteLine("server is null");
                continue;
                
            }

            //update
            UpdateLoop();                        
        }        
    }

    //when packet recieved,
    //
    internal void OnPacketRecieved(RequestRPC req)
    {
        rpcManager.DoAction(req);                     
    }

    void UpdateLoop()
    {
        UpdateLogic.ForceNotify();
    }
}