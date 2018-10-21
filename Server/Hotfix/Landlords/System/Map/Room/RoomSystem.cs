using ETModel;

namespace ETHotfix
{
    public static class RoomSystem
    {
        /// <summary>
        /// 添加玩家
        /// </summary>
        /// <param name="gamer"></param>
        public static void Add(this Room self, Gamer gamer)
        {
            int seatIndex = self.GetEmptySeat();
            //玩家需要获取一个座位坐下
            if (seatIndex >= 0)
            {
                self.gamers[seatIndex] = gamer;
                self.seats[gamer.UserID] = seatIndex;

                gamer.RoomID = self.InstanceId;
            }
        }

        /// <summary>
        /// 获取玩家
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Gamer Get(this Room self, long id)
        {
            int seatIndex = self.GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                return self.gamers[seatIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取所有玩家
        /// </summary>
        /// <returns></returns>
        public static Gamer[] GetAll(this Room self)
        {
            return self.gamers;
        }

        /// <summary>
        /// 获取玩家座位索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetGamerSeat(this Room self, long id)
        {
            if (self.seats.TryGetValue(id, out int seatIndex))
            {
                return seatIndex;
            }

            return -1;
        }

        /// <summary>
        /// 移除玩家并返回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Gamer Remove(this Room self, long id)
        {
            int seatIndex = self.GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                Gamer gamer = self.gamers[seatIndex];
                self.gamers[seatIndex] = null;
                self.seats.Remove(id);

                gamer.RoomID = 0;
                return gamer;
            }

            return null;
        }

        /// <summary>
        /// 获取空座位
        /// </summary>
        /// <returns>返回座位索引，没有空座位时返回-1</returns>
        public static int GetEmptySeat(this Room self)
        {
            for (int i = 0; i < self.gamers.Length; i++)
            {
                if (self.gamers[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"></param>
        public static void Broadcast(this Room self, IActorMessage message)
        {
            foreach (Gamer gamer in self.gamers)
            {
                if (gamer == null || gamer.isOffline)
                {
                    continue;
                }
                ActorMessageSender actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorMessageSender();
                actorProxy.Send(message);
            }
        }
    }
}
