public class Singletone<T> where T : new()
{
    public readonly static T Instance = new T();
}