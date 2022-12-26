using ET.Landlords;
using System;


namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Friend)]
        public class C2F_GetFriendsHandler : AMActorRpcHandler<FriendUnitEntity, C2F_GetFriends, F2C_GetFriends>
        {
            protected override async ETTask Run(FriendUnitEntity unit, C2F_GetFriends request, F2C_GetFriends response, Action reply)
            {
                //查询玩家好友数据
                

                reply();
                await ETTask.CompletedTask;
            }
        }
    }
}