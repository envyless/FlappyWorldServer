

using System.Collections.Generic;
using Google.Protobuf;
public class UsersTest
{
    public List<ReqUserUpdate> users = new List<ReqUserUpdate>();
    public UsersTest(MainGameEngine engine)
    {
        for(int i = 0; i < 20; ++i)
        {
            var userReq = new ReqUserUpdate();

            var ai = ActorMaker<AIUser>.New(engine);

            var user = ai.user;
            userReq.User = user;
            user.UserId = "test server ai : "+ i;
            user.X = -1.8f;
            user.Y = 0;
            user.IsDead = false;
            users.Add(userReq);
            engine.userManager.UpdateUser(userReq);
        }        
    }
}