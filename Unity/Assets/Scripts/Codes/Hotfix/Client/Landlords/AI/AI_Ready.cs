using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        public class AI_Ready : AAIHandler
        {
            public override int Check(AIComponent aiComponent, AIConfig aiConfig)
            {
                Scene scene = aiComponent.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomUnitEntity unit = roomComponent?.GetMyUnit();
                if (roomComponent != null && roomComponent.Room.Status == ERoomStatus.None
                    && unit != null && unit.Status != ELandlordStatus.Ready)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
            {
                RoomHelper.Ready(aiComponent.DomainScene());
                await ETTask.CompletedTask;
            }
        }
    }
}
