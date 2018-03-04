using Model;

namespace Hotfix
{
    [Event(EventIdType.LandlordsInitSceneStart)]
    public class InitSceneStart_CreateLandlordsLogin : AEvent
    {
        public override void Run()
        {
            //创建登录界面
            UI ui = Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsLogin);
        }
    }
}
