using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Actor_Trusteeship_NttHandler : AMActorHandler<Gamer, Actor_Trusteeship_Ntt>
    {
        protected override void Run(Gamer gamer, Actor_Trusteeship_Ntt message)
        {
            Room room = Game.Scene.GetComponent<RoomComponent>().Get(gamer.RoomID);
            //是否已经托管
            bool isTrusteeship = gamer.GetComponent<TrusteeshipComponent>() != null;
            if (message.IsTrusteeship && !isTrusteeship)
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
            Actor_Trusteeship_Ntt transpond = new Actor_Trusteeship_Ntt();
            transpond.IsTrusteeship = message.IsTrusteeship;
            transpond.UserID = gamer.UserID;
            //转发消息
            room.Broadcast(transpond);

            if (isTrusteeship)
            {
                OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
                if (gamer.UserID == orderController.CurrentAuthority)
                {
                    bool isFirst = gamer.UserID == orderController.Biggest;
                    ActorMessageSender actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorMessageSender();
                    actorProxy.Send(new Actor_AuthorityPlayCard_Ntt() { UserID = orderController.CurrentAuthority, IsFirst = isFirst });
                }
            }
        }
    }
}
