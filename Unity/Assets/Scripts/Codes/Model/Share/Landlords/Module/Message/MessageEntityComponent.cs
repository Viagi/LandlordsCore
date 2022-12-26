namespace ET
{
    namespace Landlords
    {
        [ComponentOf(typeof(Scene))]
        public class MessageEntityComponent : Entity, IAwake
        {
            [StaticField]
            public static MessageEntityComponent Instance;
        }
    }
}
