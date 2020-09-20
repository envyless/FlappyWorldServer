
// Managed by Game Engine, have Update 
public abstract class Actor{
    public MainGameEngine engine{get; private set;}
    
    //when make actor, need engine
    public void SetMainGameEngine(MainGameEngine _engine)
    {
        engine = _engine;
        SetUp();
    }        

    //init
    public abstract void SetUp();

    ~Actor(){
        engine.allActors.Remove(this);
    }
}