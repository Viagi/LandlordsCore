using ET.EventType.Landlords;
using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        [MessageHandler(SceneType.Client)]
        public class Actor_GameEndHandler : AMHandler<Actor_GameEnd>
        {
            protected override async ETTask Run(Session session, Actor_GameEnd message)
            {
                Scene scene = session.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                if(room != null)
                {
                    room.Status = ERoomStatus.None;
                    for (int i = 0; i < message.Entitys.Count; i++)
                    {
                        RoomUnitEntity unit = MongoHelper.Deserialize<RoomUnitEntity>(message.Entitys[i]);
                        room.RemoveChild(unit.Id);
                        room.AddChild(unit);
                        unit.ShowAllCards();
                    }

                    Entity component = MongoHelper.Deserialize<AccountComponent>(message.Component);
                    scene.RemoveComponent(component.GetType());
                    scene.AddComponent(component);
                    EventSystem.Instance.Publish(scene, new GameEnd() { Results = message.Results });
                }

                await ETTask.CompletedTask;
            }
        }
    }
}
