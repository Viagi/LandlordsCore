namespace ET.Client
{
	namespace Landlords
	{
		[Event(SceneType.Client)]
		public class AppStartInitFinish_CreateLoginUI : AEvent<EventType.Landlords.AppStartInitFinish>
		{
			protected override async ETTask Run(Scene scene, EventType.Landlords.AppStartInitFinish args)
			{
				await UIHelper.Create(scene, UIType.Login, UILayer.Mid);
			}
		}
	}
}
