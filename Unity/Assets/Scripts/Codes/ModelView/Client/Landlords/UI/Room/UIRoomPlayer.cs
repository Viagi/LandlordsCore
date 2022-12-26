using ET.Landlords;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ChildOf(typeof(UIRoomComponent))]
        public class UIRoomPlayer : Entity, IAwake<GameObject>, IUpdate
        {
            public List<HandCard> SelectCards = new List<HandCard>();
            public bool IsClient { get; set; }

            public GameObject Root { get; set; }
            public Image HeadPhoto;
            public GameObject HandCards;
            public GameObject PlayCards;
            public Text Money;
            public Text Name;
            public Text Prompt;
            public Text PokerNum;
            public GameObject Timer;
            public Text Time;
            public LandlordTimeoutComponent Timeout;
        }
    }
}
