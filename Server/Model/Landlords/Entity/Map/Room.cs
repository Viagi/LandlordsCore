using System.Collections.Generic;
using System.Linq;

namespace Model
{
    /// <summary>
    /// 房间状态
    /// </summary>
    public enum RoomState : byte
    {
        Idle,       
        Ready,      
        Game        
    }

    /// <summary>
    /// 房间对象
    /// </summary>
    public sealed class Room : Entity
    {
        private readonly Dictionary<long, int> seats = new Dictionary<long, int>();
        private readonly Gamer[] gamers = new Gamer[3];

        //房间状态
        public RoomState State { get; set; } = RoomState.Idle;

        //房间玩家数量
        public int Count { get { return seats.Values.Count; } }

        /// <summary>
        /// 添加玩家
        /// </summary>
        /// <param name="gamer"></param>
        public void Add(Gamer gamer)
        {
            int seatIndex = GetEmptySeat();
            //玩家需要获取一个座位坐下
            if (seatIndex >= 0)
            {
                gamers[seatIndex] = gamer;
                seats[gamer.UserID] = seatIndex;

                gamer.RoomID = this.Id;
            }
        }

        /// <summary>
        /// 获取玩家
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Gamer Get(long id)
        {
            int seatIndex = GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                return gamers[seatIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取所有玩家
        /// </summary>
        /// <returns></returns>
        public Gamer[] GetAll()
        {
            return gamers;
        }

        /// <summary>
        /// 获取玩家座位索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetGamerSeat(long id)
        {
            if (seats.TryGetValue(id, out int seatIndex))
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
        public Gamer Remove(long id)
        {
            int seatIndex = GetGamerSeat(id);
            if (seatIndex >= 0)
            {
                Gamer gamer = gamers[seatIndex];
                gamers[seatIndex] = null;
                seats.Remove(id);

                gamer.RoomID = 0;
                return gamer;
            }

            return null;
        }

        /// <summary>
        /// 获取空座位
        /// </summary>
        /// <returns>返回座位索引，没有空座位时返回-1</returns>
        public int GetEmptySeat()
        {
            for (int i = 0; i < gamers.Length; i++)
            {
                if (gamers[i] == null)
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
        public void Broadcast(IMessage message)
        {
            foreach (Gamer gamer in gamers)
            {
                if (gamer == null || gamer.isOffline)
                {
                    continue;
                }
                ActorProxy actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                actorProxy.Send(message);
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            seats.Clear();

            for (int i = 0; i < gamers.Length; i++)
            {
                if (gamers[i] != null)
                {
                    gamers[i].Dispose();
                    gamers[i] = null;
                }
            }

            State = RoomState.Idle;
        }
    }
}
