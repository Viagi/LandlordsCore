using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class MH2MP_CreateRoom_ReqHandler : AMRpcHandler<MH2MP_CreateRoom_Req, MP2MH_CreateRoom_Ack>
    {
        protected override async void Run(Session session, MH2MP_CreateRoom_Req message, Action<MP2MH_CreateRoom_Ack> reply)
        {
            MP2MH_CreateRoom_Ack response = new MP2MH_CreateRoom_Ack();
            try
            {
                //创建房间
                Room room = ComponentFactory.Create<Room>();
                room.AddComponent<DeckComponent>();
                room.AddComponent<DeskCardsCacheComponent>();
                room.AddComponent<OrderControllerComponent>();
                room.AddComponent<GameControllerComponent, RoomConfig>(RoomHelper.GetConfig(RoomLevel.Lv100));
                await room.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<RoomComponent>().Add(room);

                Log.Info($"创建房间{room.InstanceId}");

                response.RoomID = room.InstanceId;

                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
