namespace ET.Client
{
    namespace Landlords
    {
        [Event(SceneType.Client)]
        public class LoginFinish_RemoveLoginUI : AEvent<EventType.Landlords.LoginFinish>
        {
            protected override async ETTask Run(Scene scene, EventType.Landlords.LoginFinish args)
            {
                await UIHelper.Remove(scene, UIType.Login);
            }
        }
    }
}
