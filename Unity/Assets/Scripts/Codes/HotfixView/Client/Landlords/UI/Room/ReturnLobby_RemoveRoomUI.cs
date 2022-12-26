namespace ET.Client
{
	namespace Landlords
	{
		[Event(SceneType.Client)]
		public class ReturnLobby_RemoveRoomUI : AEvent<EventType.Landlords.ReturnLobby>
		{
			protected override async ETTask Run(Scene scene, EventType.Landlords.ReturnLobby args)
			{
				using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
				{
					await UIHelper.Remove(scene, UIType.Room);
				}
			}
		}
	}
}
