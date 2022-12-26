using ET.Landlords;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class MatchUnitManagerComponentUpdateSystem : UpdateSystem<MatchUnitManagerComponent>
        {
            protected override void Update(MatchUnitManagerComponent self)
            {
                self.Update();
            }
        }

        [FriendOf(typeof(MatchUnitManagerComponent))]
        public static class MatchUnitManagerComponentSystem
        {
            public static void Update(this MatchUnitManagerComponent self)
            {
                ListComponent<MatchUnitEntity> joinUnits = ListComponent<MatchUnitEntity>.Create();
                foreach (List<MatchUnitEntity> units in self.Matchers.Values)
                {
                    int count = units.Count;
                    for (int i = 0; i < count; i++)
                    {
                        ListComponent<MatchUnitEntity> room;
                        MatchUnitEntity unit = units[i];
                        long roomId = unit.RoomId;

                        if (unit.RoomId == -1)
                        {
                            //正在进入房间的跳过
                            continue;
                        }
                        else if (self.ReadyRooms.TryGetValue(roomId, out room))
                        {
                            if (room.Count == 3)
                            {
                                //有房间且无空位跳过
                                continue;
                            }
                        }
                        else
                        {
                            //无房间加入匹配
                            joinUnits.Add(unit);
                        }

                        //搜索匹配对象
                        for (int j = i + 1; j < count; j++)
                        {
                            MatchUnitEntity matcher = units[j];
                            if (matcher.RoomId == -1)
                            {
                                //正在进入房间的跳过
                                continue;
                            }
                            else if (matcher.RoomId == 0 || roomId == 0)
                            {
                                //匹配对象必须是无房间或者新房间
                                if (matcher.RoomId != 0)
                                {
                                    ListComponent<MatchUnitEntity> newRoom;
                                    if (self.ReadyRooms.TryGetValue(matcher.RoomId, out newRoom) && newRoom.Count < 3)
                                    {
                                        //新房间必须有空位
                                        roomId = matcher.RoomId;
                                        room = newRoom;
                                    }
                                }
                                else
                                {
                                    //匹配对象无房间加入匹配
                                    joinUnits.Add(matcher);
                                }
                                int number = joinUnits.Count;
                                if (room != null)
                                {
                                    //加上当前房间人数
                                    number += room.Count;
                                }
                                if (number == 3)
                                {
                                    //匹配成功
                                    self.OnMatch(joinUnits, roomId).Coroutine();
                                    joinUnits = ListComponent<MatchUnitEntity>.Create();
                                    break;
                                }
                            }
                        }
                        joinUnits.Clear();
                    }
                }

                joinUnits.Dispose();
            }

            private static async ETTask OnMatch(this MatchUnitManagerComponent self, ListComponent<MatchUnitEntity> units, long roomId)
            {
                foreach (MatchUnitEntity unit in units)
                {
                    unit.RoomId = -1;
                }
                if (roomId == 0)
                {
                    //分配房间
                    roomId = await self.GetRoom();
                }

                //加入房间
                await self.EnterRoom(units, roomId);
                units.Dispose();
            }

            private static long GetMatchLevel(this MatchUnitManagerComponent self, MatchUnitEntity entity)
            {
                long rank = entity.GetRank();
                long time = TimeHelper.ServerNow() - entity.MatchTime;
                long level = 0;
                long range = 10000;
                while (rank / range > 0)
                {
                    level += 1;
                    range *= 10;
                }

                //每等待10秒加大一个匹配分差
                level -= time / 1000 / 10;
                if (level < 0)
                {
                    level = 0;
                }

                return level;
            }

            public static void AddMatch(this MatchUnitManagerComponent self, MatchUnitEntity entity)
            {
                long level = self.GetMatchLevel(entity);
                entity.MatchLevel = level;
                entity.MatchTime = TimeHelper.ServerNow();
                self.Matchers.Add(level, entity);
            }

            public static void RemoveMatch(this MatchUnitManagerComponent self, MatchUnitEntity entity)
            {
                self.Matchers.Remove(entity.MatchLevel, entity);
                entity.MatchLevel = 0;
                entity.MatchTime = 0;
            }

            public static void RoomStart(this MatchUnitManagerComponent self, long roomId)
            {
                ListComponent<MatchUnitEntity> room;
                if (self.ReadyRooms.TryGetValue(roomId, out room))
                {
                    self.ReadyRooms.Remove(roomId);
                    self.PlayingRooms.Add(roomId, room);
                    //房间开始移除匹配
                    foreach (MatchUnitEntity unit in room)
                    {
                        self.RemoveMatch(unit);
                        unit.IsPlaying = true;
                    }
                }
            }

            public static void RoomEnd(this MatchUnitManagerComponent self, long roomId)
            {
                ListComponent<MatchUnitEntity> room;
                if (self.PlayingRooms.TryGetValue(roomId, out room))
                {
                    self.PlayingRooms.Remove(roomId);
                    self.ReadyRooms.Add(roomId, room);

                    for (int i = 0; i < room.Count; i++)
                    {
                        MatchUnitEntity unit = room[i];
                        if (unit.IsOffline)
                        {
                            //移除离线
                            self.ExitRoom(unit);
                            self.RemoveChild(unit.Id);
                            i--;
                        }
                        else
                        {
                            //在线加入匹配
                            self.AddMatch(unit);
                            unit.IsPlaying = false;
                        }
                    }
                }
            }

            public static async ETTask<long> GetRoom(this MatchUnitManagerComponent self)
            {
                StartSceneConfig config = AddressHelper.GetRoom(self.DomainZone());
                R2M_GetRoom r2M_GetRoom = (R2M_GetRoom)await ActorMessageSenderComponent.Instance.Call(config.InstanceId, new M2R_GetRoom());
                self.ReadyRooms.Add(r2M_GetRoom.RoomId, ListComponent<MatchUnitEntity>.Create());

                return r2M_GetRoom.RoomId;
            }

            public static async ETTask EnterRoom(this MatchUnitManagerComponent self, IList<MatchUnitEntity> units, long roomId)
            {
                ListComponent<MatchUnitEntity> room;
                if (self.ReadyRooms.TryGetValue(roomId, out room) || self.PlayingRooms.TryGetValue(roomId, out room))
                {
                    Actor_JoinRoomRequest joinRoomRequest = new Actor_JoinRoomRequest();
                    joinRoomRequest.UserIds = new List<long>();
                    joinRoomRequest.Entitys = new List<byte[]>();
                    foreach (MatchUnitEntity unit in units)
                    {
                        if (!unit.IsDisposed)
                        {
                            if (!room.Contains(unit))
                            {
                                room.Add(unit);
                                unit.RoomId = roomId;
                            }
                            joinRoomRequest.UserIds.Add(unit.Id);
                            joinRoomRequest.Entitys.Add(MongoHelper.Serialize(unit.GetComponent<UnitGateComponent>()));
                        }
                    }
                    if (joinRoomRequest.UserIds.Count > 0)
                    {
                        await ActorMessageSenderComponent.Instance.Call(roomId, joinRoomRequest);
                    }
                }
            }

            public static void ExitRoom(this MatchUnitManagerComponent self, MatchUnitEntity entity)
            {
                ListComponent<MatchUnitEntity> room;
                if (self.ReadyRooms.TryGetValue(entity.RoomId, out room))
                {
                    room.Remove(entity);
                    if (room.Count == 0)
                    {
                        self.ReadyRooms.Remove(entity.RoomId);
                    }
                }
            }
        }
    }
}
