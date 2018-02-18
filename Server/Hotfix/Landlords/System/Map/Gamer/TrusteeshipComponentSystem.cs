using Model;

namespace Hotfix
{
    [ObjectSystem]
    public class TrusteeshipComponentEvent : ObjectSystem<TrusteeshipComponent>, IStart
    {
        public void Start()
        {
            this.Get().Start();
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

            while (true)
            {
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);

                if (self.Id == 0)
                {
                    return;
                }

                if (gamer.UserID != orderController?.CurrentAuthority)
                {
                    continue;
                }

                ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(gamer.Id);
                //当还没抢地主时随机抢地主
                if (gamer.GetComponent<HandCardsComponent>().AccessIdentity == Identity.None)
                {
                    int randomSelect = RandomHelper.RandomNumber(0, 2);
                    actorProxy.Send(new Actor_GamerGrabLandlordSelect_Ntt() { IsGrab = randomSelect == 0 });
                    self.Playing = false;
                    continue;
                }

                //自动提示出牌
                Actor_GamerPrompt_Ack response = await actorProxy.Call<Actor_GamerPrompt_Ack>(new Actor_GamerPrompt_Req());
                if (response.Error > 0 || response.Cards == null)
                {
                    actorProxy.Send(new Actor_GamerDontPlay_Ntt());
                }
                else
                {
                    await actorProxy.Call<Actor_GamerPlayCard_Ack>(new Actor_GamerPlayCard_Req() { Cards = response.Cards });
                }
            }
        }
    }
}
