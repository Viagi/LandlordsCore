using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ComponentOf(typeof(UI))]
        public class UILoginComponent : Entity, IAwake
        {
            public InputField account;
            public InputField password;
            public Button loginBtn;
            public Button registerBtn;
            public Text prompt;
            public Text hotfixPrompt;
        }
    }
}