using System;
using System.Collections.Generic;
using Google.Protobuf;

public class UserMnanger : Singletone<UserMnanger>
{

    public Dictionary<string, ReqUserUpdate> dictUsers = new Dictionary<string, ReqUserUpdate>();

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
}