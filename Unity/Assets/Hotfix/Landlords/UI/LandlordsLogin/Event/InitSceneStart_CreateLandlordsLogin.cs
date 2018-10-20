using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.LandlordsInitSceneStart)]
    public class InitSceneStart_CreateLandlordsLogin : AEvent
    {
        public override void Run()
        {
            //创建登录界面
            UI ui = Game.Scene.GetComponent<UIComponent>().Create(UIType.LandlordsLogin);
        }
    }
}
