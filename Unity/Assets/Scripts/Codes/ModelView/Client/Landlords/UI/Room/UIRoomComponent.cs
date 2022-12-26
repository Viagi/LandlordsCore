using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	namespace Landlords
	{
		[ComponentOf(typeof(UI))]
		public class UIRoomComponent : Entity, IAwake
		{
            public readonly Dictionary<long, UIRoomPlayer> Players = new Dictionary<long, UIRoomPlayer>();
			public GameObject Root { get; set; }
            public List<Image> LandPokers;
			public GameObject Desk;
			public GameObject MatchPrompt;
			public Button QuitBtn;
			public Button ReadyBtn;
			public Text Multiples;
			public ReferenceCollector Atlas;

            public GameObject End;
			public GameObject Win;
			public GameObject Lose;
			public GameObject GamerContent;
			public Button ContinueBtn;
        }
	}
}
