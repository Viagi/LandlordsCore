using System.Linq;
using System.Collections.Generic;
using ETModel;

namespace ETHotfix
{
    public static class MatchRoomComponentSystem
    {
        /// <summary>
        /// 添加匹配房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="room"></param>
        public static void Add(this MatchRoomComponent self, Room room)
        {
            self.rooms.Add(room.Id, room);
            self.idleRooms.Enqueue(room);
        }

        /// <summary>
        /// 回收匹配房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void Recycle(this MatchRoomComponent self, long id)
        {
            Room room = self.readyRooms[id];
            self.readyRooms.Remove(room.Id);
            self.idleRooms.Enqueue(room);
        }

        /// <summary>
        /// 获取匹配房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Room Get(this MatchRoomComponent self, long id)
        {
            Room room;
            self.rooms.TryGetValue(id, out room);
            return room;
        }

        /// <summary>
        /// 获取等待中的匹配房间
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Room GetReadyRoom(this MatchRoomComponent self)
        {
            return self.readyRooms.Where(p => p.Value.Count < 3).FirstOrDefault().Value;
        }

        /// <summary>
        /// 获取空闲的匹配房间
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Room GetIdleRoom(this MatchRoomComponent self)
        {
            if (self.IdleRoomCount > 0)
            {
                Room room = self.idleRooms.Dequeue();
                self.readyRooms.Add(room.Id, room);
                return room;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 匹配房间开始游戏
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void RoomStartGame(this MatchRoomComponent self, long id)
        {
            Room room = self.readyRooms[id];
            self.readyRooms.Remove(id);
            self.gameRooms.Add(room.Id, room);
        }

        /// <summary>
        /// 匹配房间结束游戏
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void RoomEndGame(this MatchRoomComponent self, long id)
        {
            Room room = self.gameRooms[id];
            self.gameRooms.Remove(id);
            self.readyRooms.Add(room.Id, room);
        }
    }
}
