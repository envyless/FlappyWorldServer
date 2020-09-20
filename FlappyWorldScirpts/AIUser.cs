using Google.Protobuf;
using System;

public class AIUser : Actor{
    public User user = new User();

    

    public override void SetUp()
    {
        Random random = new Random();
        engine.UpdateLogic.Subscribe(_=>{                        
            user.X += engine.deltaTime * random.Next(1, 3);;            
            user.Y += engine.deltaTime * random.Next(-3, 3);
        });        
    }

    
}