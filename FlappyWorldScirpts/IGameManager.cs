
public abstract class AbsGameManager{
    public MainGameEngine engine{get; private set;}

    public AbsGameManager(MainGameEngine _engine)
    {
        engine = _engine;
        SetUp();
    }

    protected abstract void SetUp();
}