using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ComponentOf(typeof(UIRoomComponent))]
        public class InteractionComponent : Entity, IAwake
        {
            public GameObject Root;
            public Button CallBtn;
            public Button NotCallBtn;
            public Button GrabBtn;
            public Button DisgrabBtn;
            public Button PlayBtn;
            public Button PromptBtn;
            public Button DiscardBtn;
            public Button TrustBtn;
            public Button CancelTrustBtn;
        }
    }
}
