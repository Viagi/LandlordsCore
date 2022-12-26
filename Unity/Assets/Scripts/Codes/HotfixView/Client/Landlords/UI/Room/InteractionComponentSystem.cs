using ET.Landlords;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class InteractionComponentAwakeSystem : AwakeSystem<InteractionComponent>
        {
            protected override void Awake(InteractionComponent self)
            {
                self.Root = self.GetParent<UIRoomComponent>().Root.Get<GameObject>("Interaction");
                ReferenceCollector rc = self.Root.GetComponent<ReferenceCollector>();
                self.CallBtn = rc.Get<GameObject>("CallButton").GetComponent<Button>();
                self.NotCallBtn = rc.Get<GameObject>("NotCallButton").GetComponent<Button>();
                self.GrabBtn = rc.Get<GameObject>("GrabButton").GetComponent<Button>();
                self.DisgrabBtn = rc.Get<GameObject>("DisgrabButton").GetComponent<Button>();
                self.PlayBtn = rc.Get<GameObject>("PlayButton").GetComponent<Button>();
                self.PromptBtn = rc.Get<GameObject>("PromptButton").GetComponent<Button>();
                self.DiscardBtn = rc.Get<GameObject>("DiscardButton").GetComponent<Button>();
                self.TrustBtn = rc.Get<GameObject>("TrustButton").GetComponent<Button>();
                self.CancelTrustBtn = rc.Get<GameObject>("CancelTrustButton").GetComponent<Button>();

                self.Awake();
            }
        }

        [FriendOf(typeof(InteractionComponent))]
        public static class InteractionComponentSystem
        {
            public static void Awake(this InteractionComponent self)
            {
                self.CallBtn.onClick.RemoveAllListeners();
                self.CallBtn.onClick.AddListener(self.OnCall);
                self.NotCallBtn.onClick.RemoveAllListeners();
                self.NotCallBtn.onClick.AddListener(self.OnNotCall);
                self.GrabBtn.onClick.RemoveAllListeners();
                self.GrabBtn.onClick.AddListener(self.OnGrab);
                self.DisgrabBtn.onClick.RemoveAllListeners();
                self.DisgrabBtn.onClick.AddListener(self.OnDisgrab);
                self.PlayBtn.onClick.RemoveAllListeners();
                self.PlayBtn.onClick.AddListener(self.OnPlayCard);
                self.PromptBtn.onClick.RemoveAllListeners();
                self.PromptBtn.onClick.AddListener(self.OnPrompt);
                self.DiscardBtn.onClick.RemoveAllListeners();
                self.DiscardBtn.onClick.AddListener(self.OnDiscard);
                self.TrustBtn.onClick.RemoveAllListeners();
                self.TrustBtn.onClick.AddListener(self.OnTrust);
                self.CancelTrustBtn.onClick.RemoveAllListeners();
                self.CancelTrustBtn.onClick.AddListener(self.OnCancelTrust);

                self.Root.SetActive(false);
            }

            public static void Refresh(this InteractionComponent self, RoomComponent roomComponent)
            {
                RoomEntity room = roomComponent.Room;
                RoomUnitEntity currentUnit = room.GetCurrent();
                RoomUnitEntity activeUnit = room.GetActive();
                RoomUnitEntity myUnit = roomComponent.GetMyUnit();
                bool current = currentUnit == myUnit;
                bool first = activeUnit == myUnit;

                self.Root.SetActive(true);
                self.CallBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.CallLandlord);
                self.NotCallBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.CallLandlord);
                self.GrabBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.RobLandlord);
                self.DisgrabBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.RobLandlord);
                self.PlayBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.PlayCard);
                self.DiscardBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.PlayCard && !first);
                self.PromptBtn.gameObject.SetActive(current && !myUnit.IsTrust && room.Status == ERoomStatus.PlayCard);
                self.TrustBtn.gameObject.SetActive(!myUnit.IsTrust && room.Status == ERoomStatus.PlayCard);
                self.CancelTrustBtn.gameObject.SetActive(myUnit.IsTrust && room.Status == ERoomStatus.PlayCard);
            }

            private static void OnCall(this InteractionComponent self)
            {
                self.CallBtn.gameObject.SetActive(false);
                self.NotCallBtn.gameObject.SetActive(false);
                RoomHelper.CallLandlord(self.DomainScene(), true);
            }

            private static void OnNotCall(this InteractionComponent self)
            {
                self.CallBtn.gameObject.SetActive(false);
                self.NotCallBtn.gameObject.SetActive(false);
                RoomHelper.CallLandlord(self.DomainScene(), false);
            }

            private static void OnGrab(this InteractionComponent self)
            {
                self.GrabBtn.gameObject.SetActive(false);
                self.DisgrabBtn.gameObject.SetActive(false);
                RoomHelper.RobLandlord(self.DomainScene(), true);
            }

            private static void OnDisgrab(this InteractionComponent self)
            {
                self.GrabBtn.gameObject.SetActive(false);
                self.DisgrabBtn.gameObject.SetActive(false);
                RoomHelper.RobLandlord(self.DomainScene(), false);
            }

            private static void OnPlayCard(this InteractionComponent self)
            {
                Scene scene = self.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent.Room;
                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                UIRoomPlayer player = uiRoomComponent.GetMyPlayer();
                List<HandCard> cards = player.GetSelectedCards();
                CardGroupType type;

                if (cards.Count > 0 && room.CheckPlayCards(roomComponent.GetMyUnit(), cards, out type))
                {
                    RoomHelper.PlayCards(scene, cards);
                    player.PlayCards(true);
                }
                else
                {
                    player.PlayCards(false);
                }
            }

            private static void OnDiscard(this InteractionComponent self)
            {
                Scene scene = self.DomainScene();
                RoomHelper.PlayCards(scene);
            }

            private static void OnPrompt(this InteractionComponent self)
            {
                Scene scene = self.DomainScene();
                RoomComponent roomComponent = scene.GetComponent<RoomComponent>();
                RoomEntity room = roomComponent.Room;
                RoomUnitEntity myUnit = roomComponent.GetMyUnit();
                RoomUnitEntity activeUnit = room.GetActive();
                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                UIRoomPlayer player = uiRoomComponent.GetMyPlayer();

                using (ListComponent<HandCard> cards = ET.Landlords.RoomHelper.SearchCards(activeUnit.PlayCards, myUnit.HandCards))
                {
                    if (cards.Count == 0)
                    {
                        return;
                    }

                    player.UnSelected();
                    player.SelectedCards(cards);
                    player.Refresh(myUnit);
                }
            }

            private static void OnTrust(this InteractionComponent self)
            {
                RoomHelper.Trust(self.DomainScene(), true);
            }

            private static void OnCancelTrust(this InteractionComponent self)
            {
                RoomHelper.Trust(self.DomainScene(), false);
            }
        }
    }
}
