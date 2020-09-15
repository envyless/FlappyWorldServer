

using System;
using System.Collections.Generic;
using Google.Protobuf;

public class UsersTest
{
    public List<ReqUserUpdate> users = new List<ReqUserUpdate>();
    public UsersTest()
    {
        for(int i = 0; i < 20; ++i)
        {
            var user = new ReqUserUpdate();
            user.UserId = "test server ai : "+ i;
            user.X = -1.8f;
            user.Y = 0;
            user.IsDead = false;
            users.Add(user);
            UserMnanger.Instance.UpdateUser(user);
        }

        MainGameEngine.Instance.UpdateLogic.Subscribe(_=>{
            UpdateTestUser();
        });
    }

    public void UpdateTestUser()
    {
        Random random = new Random();
        foreach(var user in users)
        {
            user.X += MainGameEngine.deltaTime * random.Next(1, 3);;
            
            user.Y += MainGameEngine.deltaTime * random.Next(-3, 3);
        }
    }
}