using ET.Landlords;
using System.Collections.Generic;

namespace ET.Client
{
    namespace Landlords
    {
        public static class RoomHelper
        {
            public static void Ready(Scene clientScene)
            {
                clientScene.GetComponent<SessionComponent>().Session.Send(new Actor_PlayerReady());
            }

            public static void CallLandlord(Scene clientScene, bool isCall)
            {
                clientScene.GetComponent<SessionComponent>().Session.Send(new Actor_CallLandlord() { CallLandlord = isCall });
            }

            public static void RobLandlord(Scene clientScene, bool isRob)
            {
                clientScene.GetComponent<SessionComponent>().Session.Send(new Actor_RobLandlord() { RobLandlord = isRob });
            }

            public static void PlayCards(Scene clientScene, List<HandCard> cards = null)
            {
                clientScene.GetComponent<SessionComponent>().Session.Send(new Actor_PlayCards() { Cards = cards, ShowCards = cards != null && cards.Count > 0 });
            }

            public static void Trust(Scene clientScene, bool isTrust)
            {
                clientScene.GetComponent<SessionComponent>().Session.Send(new Actor_Trust() { IsTrust = isTrust });
            }
        }
    }
}
