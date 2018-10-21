using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 房间管理组件
    /// </summary>
    public class RoomComponent : Component
    {
        private readonly Dictionary<long, Room> rooms = new Dictionary<long, Room>();

        /// <summary>
        /// 添加房间
        /// </summary>
        /// <param name="room"></param>
        public void Add(Room room)
        {
            this.rooms.Add(room.InstanceId, room);
        }

        /// <summary>
        /// 获取房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room Get(long id)
        {
            Room room;
            this.rooms.TryGetValue(id, out room);
            return room;
        }

        /// <summary>
        /// 移除房间并返回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room Remove(long id)
        {
            Room room = Get(id);
            this.rooms.Remove(id);
            return room;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (var room in this.rooms.Values)
            {
                room.Dispose();
            }
        }
    }
}
