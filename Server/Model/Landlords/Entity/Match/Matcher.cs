namespace Model
{
    [ObjectSystem]
    public class MatcherEvent : ObjectSystem<Matcher>, IAwake<long>
    {
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    /// <summary>
    /// 匹配对象
    /// </summary>
    public sealed class Matcher : Entity
    {
        //用户ID（唯一）
        public long UserID { get; private set; }

        //玩家GateActorID
        public long PlayerID { get; set; }

        //客户端与网关服务器的SessionID
        public long GateSessionID { get; set; }

        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }

            base.Dispose();

            this.UserID = 0;
            this.PlayerID = 0;
            this.GateSessionID = 0;
        }
    }
}
