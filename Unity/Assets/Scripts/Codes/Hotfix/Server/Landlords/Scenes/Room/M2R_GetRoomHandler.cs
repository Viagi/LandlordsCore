using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class M2R_GetRoomHandler : AMActorRpcHandler<Scene, M2R_GetRoom, R2M_GetRoom>
        {
            protected override async ETTask Run(Scene scene, M2R_GetRoom request, R2M_GetRoom response, Action reply)
            {
                RoomEntity entity = null;
                foreach (Entity child in scene.Children.Values)
                {
                    RoomEntity room = child as RoomEntity;
                    if (room != null && room.Status == ERoomStatus.None
                        && room.Children.Count == 0)
                    {
                        entity = room;
                        break;
                    }
                }

                if (entity == null)
                {
                    entity = scene.AddChild<RoomEntity, long, long, long>(500, 1, 20 * 1000);
                    entity.AddComponent<MailBoxComponent>();
                }

                response.RoomId = entity.InstanceId;
                reply();

                await ETTask.CompletedTask;
            }
        }
    }
}
