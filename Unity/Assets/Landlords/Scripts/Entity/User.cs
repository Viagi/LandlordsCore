namespace Model
{
    [ObjectSystem]
    public class UserEvent : ObjectSystem<User>, IAwake<long>
    {
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    /// <summary>
    /// 玩家对象
    /// </summary>
    public sealed class User : Entity
    {
        //用户ID（唯一）
        public long UserID { get; private set; }

        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }

            base.Dispose();

            this.UserID = 0;
        }
    }
}