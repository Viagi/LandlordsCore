namespace ET.Client
{
	namespace Landlords
	{
		[Event(SceneType.Client)]
		public class LoginFinish_CreateLobbyUI : AEvent<EventType.Landlords.LoginFinish>
		{
			protected override async ETTask Run(Scene scene, EventType.Landlords.LoginFinish args)
			{
				using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Lobby.GetHashCode()))
				{
					await UIHelper.Create(scene, UIType.Lobby, UILayer.Mid);
				}
			}
		}
	}
}
