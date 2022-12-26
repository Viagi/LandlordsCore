namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long, long>
        {
            protected override void Awake(UnitGateComponent self, long unitId, long gateSessionId)
            {
                self.UnitId = unitId;
                self.GateSessionId = gateSessionId;
            }
        }

        [FriendOf(typeof(UnitGateComponent))]
        public static class UnitGateComponentSystem
        {
            public static void SendToClient(this UnitGateComponent self, IActorMessage message)
            {
                ActorMessageSenderComponent.Instance.Send(self.GateSessionId, message);
            }

            public static void SendToUnit(this UnitGateComponent self, IActorMessage message)
            {
                ActorMessageSenderComponent.Instance.Send(self.UnitId, message);
            }

            public static async ETTask<IActorResponse> CallUnit(this UnitGateComponent self, IActorRequest request)
            {
                IActorResponse response = await ActorMessageSenderComponent.Instance.Call(self.UnitId, request);

                return response;
            }
        }
    }
}
