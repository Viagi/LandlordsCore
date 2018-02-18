namespace Model
{
    [ObjectSystem]
    public class ClientComponentEvent : ObjectSystem<ClientComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class ClientComponent : Component
    {
        public static ClientComponent Instance { get; private set; }

        public User LocalPlayer { get; set; }

        public void Awake()
        {
            Instance = this;
        }
    }
}
