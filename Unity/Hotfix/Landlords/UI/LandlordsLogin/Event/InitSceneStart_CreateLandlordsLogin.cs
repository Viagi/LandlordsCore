using Model;

namespace Hotfix
{
    [Event((int)EventIdType.LandlordsInitSceneStart)]
    public class InitSceneStart_CreateLandlordsLogin : IEvent
    {
        public void Run()
        {
            //创建登录界面
            UI ui = Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsLogin);
        }
    }
}
