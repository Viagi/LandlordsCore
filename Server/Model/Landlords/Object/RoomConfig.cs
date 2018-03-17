namespace ETModel
{
    /// <summary>
    /// 房间配置
    /// </summary>
    public struct RoomConfig
    {
        //房间初始倍率
        public int Multiples { get; set; }

        //房间底分
        public long BasePointPerMatch { get; set; }

        //房间最低门槛
        public long MinThreshold { get; set; }
    }
}
