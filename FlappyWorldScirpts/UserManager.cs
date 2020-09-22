using System;
using System.Collections.Generic;
using Google.Protobuf;

public class UserMnanger : AbsGameManager
{    
    public Dictionary<string, User> dictUsers = new Dictionary<string, User>();

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

    public override void SetUp()
    {
        engine.UpdateLogic.Subscribe(_=>{
            if(dictUsers.Count == 0)
                return;
            
            //req users updates
            RequestRPC rsp = new RequestRPC();
            ReqUsersUpdate usersUpdate = new ReqUsersUpdate();
			usersUpdate.Users.AddRange(dictUsers.Values);  
            rsp.ReqUsersUpdate = usersUpdate;    
            engine.server.Multicast(rsp.ToByteArray());
        });
    }
}