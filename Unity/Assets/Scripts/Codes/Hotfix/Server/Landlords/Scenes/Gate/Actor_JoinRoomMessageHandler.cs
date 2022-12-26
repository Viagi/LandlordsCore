using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Gate)]
        public class Actor_JoinRoomMessageHandler : AMActorHandler<Player, Actor_JoinRoomMessage>
        {
            protected override async ETTask Run(Player unit, Actor_JoinRoomMessage message)
            {
                LandlordsComponent landlordsComponent = unit.GetComponent<LandlordsComponent>();
                landlordsComponent.RoomId = message.UnitId;
                unit.GetComponent<UnitGateComponent>().SendToClient(new G2C_EnterRoom() { Entity = message.Entity, UnitId = landlordsComponent.UserId });

                await ETTask.CompletedTask;
            }
        }
    }
}
