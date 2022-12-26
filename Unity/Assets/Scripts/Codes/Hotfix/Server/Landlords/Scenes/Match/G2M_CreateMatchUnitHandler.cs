using ET.Landlords;
using System;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Match)]
        public class G2M_CreateMatchUnitHandler : AMActorRpcHandler<Scene, G2M_CreateMatchUnit, M2G_CreateMatchUnit>
        {
            protected override async ETTask Run(Scene scene, G2M_CreateMatchUnit request, M2G_CreateMatchUnit response, Action reply)
            {
                MatchUnitManagerComponent managerComponent = scene.GetComponent<MatchUnitManagerComponent>();
                MatchUnitEntity unit = managerComponent.GetChild<MatchUnitEntity>(request.UserId);
                UnitGateComponent unitGateComponent = MongoHelper.Deserialize<UnitGateComponent>(request.Entity);
                AccountComponent accountComponent = await UserHelper.AccessUserComponent<AccountComponent>(request.UserId);
                bool reconnect = false;

                if(accountComponent.Money == 0)
                {
                    response.Error = ErrorCode.ERR_MoneyNotEnough;
                    reply();
                    return;
                }

                if (unit == null)
                {
                    unit = managerComponent.AddChildWithId<MatchUnitEntity>(request.UserId);
                    unit.AddComponent(unitGateComponent);
                    unit.AddComponent(accountComponent);
                    unit.AddComponent<MailBoxComponent>();
                    unit.AddComponent<MirrorUnitComponent, long>(request.UserId);
                    managerComponent.AddMatch(unit);
                }
                else
                {
                    unit.RemoveComponent<UnitGateComponent>();
                    unit.AddComponent(unitGateComponent);
                    unit.RemoveComponent<AccountComponent>();
                    unit.AddComponent(accountComponent);
                    unit.IsOffline = false;
                    reconnect = true;
                }

                response.UnitId = unit.InstanceId;
                reply();

                if (reconnect)
                {
                    using (ListComponent<MatchUnitEntity> units = ListComponent<MatchUnitEntity>.Create())
                    {
                        units.Add(unit);
                        await managerComponent.EnterRoom(units, unit.RoomId);
                    }
                }
            }
        }
    }
}
