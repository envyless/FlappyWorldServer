
// Managed by Game Engine, have Update 
using System;
using System.Reflection;

public abstract class Actor{
    public MainGameEngine engine{get; private set;}
    
    //when make actor, need engine
    public void SetMainGameEngine(MainGameEngine _engine)
    {
        engine = _engine;        
        SetUp();
        // Type thisType = this.GetType();
        // MethodInfo theMethod = thisType.GetMethod("Update");
        // theMethod.Invoke(this, userParameters);
        // engine.setUps.Add(FirstUpdate);
    }        

    //init
    public abstract void SetUp();

    ~Actor(){
        engine.allActors.Remove(this);
    }
}