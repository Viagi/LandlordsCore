namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class StartMatch_RemoveLobbyUI : AEvent<EventType.Landlords.StartMatch>
        {
            protected override async ETTask Run(Scene scene, EventType.Landlords.StartMatch args)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Lobby.GetHashCode()))
                {
                    await UIHelper.Remove(scene, UIType.Lobby);
                }
            }
        }
    }
}
