﻿using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        public class AI_CallLandlord : AAIHandler
        {
            public override int Check(AIComponent aiComponent, AIConfig aiConfig)
            {
                Scene scene = aiComponent.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent?.Room;
                RoomUnitEntity currentUnit = room?.GetCurrent();

                if (room != null && room.Status == ERoomStatus.CallLandlord
                    && currentUnit.Id == roomComponent.MyId)
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
                await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
                if (cancellationToken.IsCancel()) return;
                RoomHelper.CallLandlord(aiComponent.DomainScene(), RandomGenerator.RandomBool());
            }
        }
    }
}