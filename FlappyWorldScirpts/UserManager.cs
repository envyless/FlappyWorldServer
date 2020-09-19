using System;
using System.Collections.Generic;
using Google.Protobuf;

public class UserMnanger : AbsGameManager
{    
    public Dictionary<string, ReqUserUpdate> dictUsers = new Dictionary<string, ReqUserUpdate>();

    public UserMnanger(MainGameEngine _engine) : base(_engine)
    {
    }

    //push or update user req
    public void UpdateUser(ReqUserUpdate reqUserUpdate)
    {
        dictUsers.TryGetValue(reqUserUpdate.UserId, out var user);
        if(user == null)
        {
            dictUsers.Add(reqUserUpdate.UserId, reqUserUpdate);
        }
        else
        {
            dictUsers[reqUserUpdate.UserId] = reqUserUpdate;
        }       
    }

    protected override void SetUp()
    {
        throw new NotImplementedException();
    }
}