using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	namespace Landlords
	{
		[ComponentOf(typeof(UI))]
		public class UILobbyComponent : Entity, IAwake
		{
			public Button matchBtn;
			public Text nickName;
			public Text money;
            public Text win;
            public Text lose;
        }
	}
}
