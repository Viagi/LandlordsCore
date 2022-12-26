using ET.Client.Landlords;
using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class Actor_PlaySwitchHandler : AMHandler<Actor_PlaySwitch>
    {
        protected override async ETTask Run(Session session, Actor_PlaySwitch message)
        {
            Scene scene = session.DomainScene();
            RoomEntity room = scene.GetComponent<RoomComponent>().Room;
            RoomUnitEntity beforeUnit = room.Get(message.Before);
            RoomUnitEntity currentUnit = room.Get(message.Current);
            room.Status = message.Status;
            room.Current = message.Current;
            room.Active = message.Active;
            room.Rate = message.Rate;

            beforeUnit.RemoveComponent<LandlordTimeoutComponent>();
            long now = TimeHelper.ServerFrameTime();
            long timeout = message.Time + room.Timeout;
            if (now < timeout)
            {
                currentUnit.RemoveComponent<LandlordTimeoutComponent>();
                currentUnit.AddComponent<LandlordTimeoutComponent, long>(timeout - now);
            }

            currentUnit.Status = ELandlordStatus.None;
            currentUnit.PlayCards.Clear();

            EventSystem.Instance.Publish(scene, new PlayerSwitch());
            await ETTask.CompletedTask;
        }
    }
}
