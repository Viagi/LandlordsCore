using ET.Landlords;
using System;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Room)]
        public class Actor_JoinRoomRequestHandler : AMActorRpcHandler<RoomEntity, Actor_JoinRoomRequest, Actor_JoinRoomResponse>
        {
            protected override async ETTask Run(RoomEntity room, Actor_JoinRoomRequest request, Actor_JoinRoomResponse response, Action reply)
            {
                using (ListComponent<RoomUnitEntity> joinPlayers = ListComponent<RoomUnitEntity>.Create())
                {
                    for (int i = 0; i < request.UserIds.Count; i++)
                    {
                        long userId = request.UserIds[i];
                        UnitGateComponent unitGateComponent = MongoHelper.Deserialize<UnitGateComponent>(request.Entitys[i]);
                        AccountComponent accountComponent = await UserHelper.AccessUserComponent<AccountComponent>(userId);
                        RoomUnitEntity player = room.GetChild<RoomUnitEntity>(userId);

                        if (player == null)
                        {
                            player = room.AddChildWithId<RoomUnitEntity>(userId);
                            room.Add(player);
                            player.AddComponent(accountComponent);
                            player.AddComponent(unitGateComponent);
                            player.AddComponent<MailBoxComponent>();
                            player.AddComponent<MirrorUnitComponent, long>(userId);
                        }
                        else
                        {
                            player.RemoveComponent<AccountComponent>();
                            player.AddComponent(accountComponent);
                            await player.Online(unitGateComponent);
                        }

                        joinPlayers.Add(player);
                    }

                    List<byte[]> entitys = new List<byte[]>();
                    byte[] roomBytes = MongoHelper.Serialize(room);

                    //通知玩家进入房间
                    foreach (RoomUnitEntity player in joinPlayers)
                    {
                        UnitGateComponent unitGateComponent = player.GetComponent<UnitGateComponent>();
                        unitGateComponent.SendToUnit(new Actor_JoinRoomMessage()
                        {
                            UnitId = player.InstanceId,
                            Entity = roomBytes
                        });
                        entitys.Add(MongoHelper.Serialize(player));
                    }

                    //广播玩家加入房间
                    foreach (RoomUnitEntity player in room.Children.Values)
                    {
                        UnitGateComponent unitGateComponent = player.GetComponent<UnitGateComponent>();
                        if (!joinPlayers.Contains(player))
                        {
                            if (room.Status == ERoomStatus.None)
                            {
                                unitGateComponent.SendToClient(new Actor_PlayerEnter() { Entitys = entitys });
                            }
                            else
                            {
                                unitGateComponent.SendToClient(new Actor_PlayerReconnect() { Entitys = entitys });
                            }
                        }
                    }

                    reply();
                }
            }
        }
    }
}
