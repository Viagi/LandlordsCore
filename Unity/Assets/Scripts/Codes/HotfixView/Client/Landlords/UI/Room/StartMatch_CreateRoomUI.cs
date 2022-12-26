namespace ET.Client
{
	namespace Landlords
	{
		[Event(SceneType.Client)]
		public class StartMatch_CreateRoomUI : AEvent<EventType.Landlords.StartMatch>
		{
			protected override async ETTask Run(Scene scene, EventType.Landlords.StartMatch args)
			{
				using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UI, UIType.Room.GetHashCode()))
				{
					await UIHelper.Create(scene, UIType.Room, UILayer.Mid);
				}
			}
		}
	}
}
