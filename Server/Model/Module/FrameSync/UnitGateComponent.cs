namespace ETModel
{
	[ObjectSystem]
	public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long>
	{
		public override void Awake(UnitGateComponent self, long a)
		{
			self.Awake(a);
		}
	}

	public class UnitGateComponent : Component, ISerializeToEntity
	{
		public long GateSessionId;

		public void Awake(long gateSessionId)
		{
			this.GateSessionId = gateSessionId;
		}

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            Game.Scene.GetComponent<ActorProxyComponent>()?.Remove(GateSessionId);

            base.Dispose();
        }

        public ActorProxy GetActorProxy()
		{
			return Game.Scene.GetComponent<ActorProxyComponent>().Get(this.GateSessionId);
		}
	}
}