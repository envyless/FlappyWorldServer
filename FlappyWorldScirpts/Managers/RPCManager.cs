using System;
using System.Collections.Generic;
using Google.Protobuf;

public class RPCManager : AbsGameManager
{
    public Dictionary<RequestRPC.RequestsOneofCase, HashSet<Action<IMessage>> > callbacks 
        = new Dictionary<RequestRPC.RequestsOneofCase, HashSet<Action<IMessage>> >();

    
    public HashSet<Action<IMessage>> GetActions(RequestRPC.RequestsOneofCase requestsOneofCase)
    {
        callbacks.TryGetValue(requestsOneofCase, out var tempCallback);
        if(tempCallback == null)
        {
            tempCallback = new HashSet<Action<IMessage>>();
            callbacks.Add(requestsOneofCase, tempCallback);            
        }
        
        return tempCallback;
    } 

    public void RegistCallback(RequestRPC.RequestsOneofCase requestsOneofCase, System.Action<IMessage> callback)
    {        
        var actions = GetActions(requestsOneofCase);
        actions.Add(callback);
    }

    public void RemoveCallback(RequestRPC.RequestsOneofCase requestsOneofCase, System.Action<IMessage> callback)
    {
        var actions = GetActions(requestsOneofCase);
        actions.Remove(callback);
    }

    public RPCManager(MainGameEngine _engine) : base(_engine)
    {
    }

    protected override void SetUp()
    {
        
    }
}