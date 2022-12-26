using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ComponentOf(typeof(UIRoomComponent))]
        public class EndComponent : Entity, IAwake
        {
            public GameObject Root;
            public GameObject Win;
            public GameObject Lose;
            public GameObject GamerContent;
            public Button ContinueBtn;
        }
    }
}
