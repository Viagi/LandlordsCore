using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Friend)]
        public class G2F_LoginFriendHandler : AMActorRpcHandler<Scene, G2F_LoginFriend, F2G_LoginFriend>
        {
            protected override async ETTask Run(Scene scene, G2F_LoginFriend request, F2G_LoginFriend response, Action reply)
            {
                FriendUnitEntity unit = scene.AddChildWithId<FriendUnitEntity>(request.UserId);
                unit.AddComponent<MailBoxComponent>();
                unit.AddComponent(MongoHelper.Deserialize<UnitGateComponent>(request.Entity));
                unit.AddComponent<MirrorUnitComponent, long>(request.UserId);

                response.UnitId = unit.InstanceId;
                reply();

                await ETTask.CompletedTask;
            }
        }
    }
}
