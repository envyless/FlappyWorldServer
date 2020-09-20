using System;
using System.Collections.Generic;
using Google.Protobuf;

public class UserMnanger : AbsGameManager
{    
    public Dictionary<string, User> dictUsers = new Dictionary<string, User>();

    public UserMnanger(MainGameEngine _engine) : base(_engine)
    {
    }

    //push or update user req
    public void UpdateUser(ReqUserUpdate reqUserUpdate)
    {
        var userObject = reqUserUpdate.User;
        dictUsers.TryGetValue(userObject.UserId, out var user);
        if(user == null)
        {
            dictUsers.Add(userObject.UserId, reqUserUpdate.User);
        }
        else
        {
            dictUsers[userObject.UserId] = reqUserUpdate.User;
        }       
    }

    protected override void SetUp()
    {
        
    }
}