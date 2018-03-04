using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_Trusteeship_NttHandler : AMActorHandler<Gamer, Actor_Trusteeship_Ntt>
    {
        protected override async Task Run(Gamer gamer, Actor_Trusteeship_Ntt message)
        {
            Room room = Game.Scene.GetComponent<RoomComponent>().Get(gamer.RoomID);
            //是否已经托管
            bool isTrusteeship = gamer.GetComponent<TrusteeshipComponent>() != null;
            if (message.isTrusteeship && !isTrusteeship)
            {
                gamer.AddComponent<TrusteeshipComponent>();
                Log.Info($"玩家{gamer.UserID}切换为自动模式");
            }
            else if (isTrusteeship)
            {
                gamer.RemoveComponent<TrusteeshipComponent>();
                Log.Info($"玩家{gamer.UserID}切换为手动模式");
            }

            //这里由服务端设置消息UserID用于转发
            message.UserID = gamer.UserID;
            //转发消息
            room.Broadcast(message);

            if (isTrusteeship)
            {
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
                if (gamer.UserID == orderController.CurrentAuthority)
                {
                    bool isFirst = gamer.UserID == orderController.Biggest;
                    ActorProxy actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                    actorProxy.Send(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = isFirst });
                }
            }

            await Task.CompletedTask;
        }
    }
}
