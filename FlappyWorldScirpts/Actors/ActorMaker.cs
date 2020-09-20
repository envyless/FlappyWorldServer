public static class ActorMaker<T> where T : Actor, new()
{
    public static T New(MainGameEngine engine){
        T actor = new T();
        actor.SetMainGameEngine(engine);
        engine.allActors.Add(actor);
        return actor;
    } 
}

