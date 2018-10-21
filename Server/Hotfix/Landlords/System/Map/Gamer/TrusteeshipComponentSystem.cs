using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class TrusteeshipComponentStartSystem : StartSystem<TrusteeshipComponent>
    {
        public override void Start(TrusteeshipComponent self)
        {
            self.Start();
        }
    }

    public static class TrusteeshipComponentSystem
    {
        public static async void Start(this TrusteeshipComponent self)
        {
            //玩家所在房间
            Room room = Game.Scene.GetComponent<RoomComponent>().Get(self.GetParent<Gamer>().RoomID);
            OrderControllerComponent orderController = room.GetComponent<OrderControllerComponent>();
            Gamer gamer = self.GetParent<Gamer>();
            bool isStartPlayCard = false;

            while (true)
            {
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);

                if (self.IsDisposed)
                {
                    return;
                }

                if (gamer.UserID != orderController?.CurrentAuthority)
                {
                    continue;
                }

                //自动出牌开关,用于托管延迟出牌
                isStartPlayCard = !isStartPlayCard;
                if (isStartPlayCard)
                {
                    continue;
                }

                ActorMessageSender actorProxy = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(gamer.InstanceId);
                //当还没抢地主时随机抢地主
                if (gamer.GetComponent<HandCardsComponent>().AccessIdentity == Identity.None)
                {
                    int randomSelect = RandomHelper.RandomNumber(0, 2);
                    actorProxy.Send(new Actor_GamerGrabLandlordSelect_Ntt() { IsGrab = randomSelect == 0 });
                    self.Playing = false;
                    continue;
                }

                //自动提示出牌
                Actor_GamerPrompt_Ack response = await actorProxy.Call(new Actor_GamerPrompt_Req()) as Actor_GamerPrompt_Ack;
                if (response.Error > 0 || response.Cards.Count == 0)
                {
                    actorProxy.Send(new Actor_GamerDontPlay_Ntt());
                }
                else
                {
                    await actorProxy.Call(new Actor_GamerPlayCard_Req() { Cards = response.Cards });
                }
            }
        }
    }
}
