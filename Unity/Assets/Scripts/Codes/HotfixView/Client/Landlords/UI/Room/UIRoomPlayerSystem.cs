using ET.Landlords;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UIRoomPlayerAwakeSystem : AwakeSystem<UIRoomPlayer, GameObject>
        {
            protected override void Awake(UIRoomPlayer self, GameObject root)
            {
                ReferenceCollector rc = root.GetComponent<ReferenceCollector>();
                self.Root = root;
                self.Name = rc.Get<GameObject>("Name").GetComponent<Text>();
                self.Prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
                self.Money = rc.Get<GameObject>("Money").GetComponent<Text>();
                self.HandCards = rc.Get<GameObject>("HandCards");
                self.PlayCards = rc.Get<GameObject>("PlayCards");
                self.HeadPhoto = rc.Get<GameObject>("HeadPhoto").GetComponent<Image>();
                self.PokerNum = self.HandCards.Get<GameObject>("PokerNum")?.GetComponent<Text>();
                self.Timer = rc.Get<GameObject>("Timer");
                self.Time = self.Timer.Get<GameObject>("Time").GetComponent<Text>();

                self.Reset();
            }
        }

        [ObjectSystem]
        public class UIRoomPlayerUpdateSystem : UpdateSystem<UIRoomPlayer>
        {
            protected override void Update(UIRoomPlayer self)
            {
                self.UpdateTimer();
            }
        }

        [FriendOf(typeof(UIRoomPlayer))]
        public static class UIRoomPlayerSystem
        {
            public static void Init(this UIRoomPlayer self, RoomEntity room,
                RoomUnitEntity unit, bool isClient)
            {
                self.IsClient = isClient;
                self.Refresh(unit);
            }

            public static void Reset(this UIRoomPlayer self)
            {
                self.Name.text = "空位";
                self.Money.text = "0";
                self.Prompt.text = string.Empty;
                self.HeadPhoto.gameObject.SetActive(false);
                self.HandCards.gameObject.SetActive(false);
                self.SelectCards.Clear();
                self.Timer.SetActive(false);
            }

            public static void Ready(this UIRoomPlayer self)
            {
                self.Prompt.text = "准备";
            }

            public static void PlayCards(this UIRoomPlayer self, bool verify)
            {
                if (verify)
                {
                    self.Prompt.text = string.Empty;
                }
                else
                {
                    self.Prompt.text = "不符合牌型，无法出牌";
                }
            }

            public static void SelectedCards(this UIRoomPlayer self, List<HandCard> cards)
            {
                self.SelectCards.AddRange(cards);
            }

            public static void UnSelected(this UIRoomPlayer self)
            {
                self.SelectCards.Clear();
            }

            public static List<HandCard> GetSelectedCards(this UIRoomPlayer self)
            {
                return self.SelectCards;
            }

            public static void Refresh(this UIRoomPlayer self, RoomUnitEntity unit)
            {
                self.RefreshAccount(unit.GetComponent<AccountComponent>());
                self.RefreshIdentity(unit.Identity);
                self.RefreshHandCards(unit.HandCards);
                self.RefreshPlayCards(unit.PlayCards);
                self.RefreshPrompt(unit.Status);
                self.RefreshTimer(unit.GetComponent<LandlordTimeoutComponent>());
            }

            private static void RefreshAccount(this UIRoomPlayer self, AccountComponent accountComponent)
            {
                self.Name.text = accountComponent.NickName;
                self.Money.text = accountComponent.Money.ToString();
            }

            private static void RefreshIdentity(this UIRoomPlayer self, ELandlordIdentity identity)
            {
                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                switch (identity)
                {
                    case ELandlordIdentity.Peasantry:
                        self.HeadPhoto.sprite = uiRoomComponent.GetSprite("Identity_Farmer");
                        break;
                    case ELandlordIdentity.Landlord:
                        self.HeadPhoto.sprite = uiRoomComponent.GetSprite("Identity_Landlord");
                        break;
                }
                self.HeadPhoto.gameObject.SetActive(identity != ELandlordIdentity.None);
            }

            private static void RefreshHandCards(this UIRoomPlayer self, List<HandCard> cards)
            {
                self.HandCards.SetActive(cards.Count > 0);

                if (!self.IsClient)
                {
                    self.PokerNum.text = cards.Count.ToString();
                    return;
                }

                for (int i = 0; i < self.SelectCards.Count; i++)
                {
                    if (!cards.Contains(self.SelectCards[i]))
                    {
                        self.SelectCards.RemoveAt(i);
                        i--;
                    }
                }

                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(RoomResourceType.HandCard.StringToAB(), RoomResourceType.HandCard);
                Transform parent = self.HandCards.transform;
                for (int i = 0; i < cards.Count; i++)
                {
                    HandCard card = cards[i];
                    GameObject item;
                    GameObject handCard;
                    if (i >= parent.childCount)
                    {
                        item = GameObject.Instantiate<GameObject>(bundleGameObject, parent);
                    }
                    else
                    {
                        item = parent.GetChild(i).gameObject;
                        item.SetActive(true);
                    }
                    item.name = card.ToString();
                    handCard = item.Get<GameObject>("Card");
                    handCard.GetComponent<Image>().sprite = uiRoomComponent.GetSprite(item.name);
                    handCard.GetComponent<Button>().onClick.RemoveAllListeners();
                    handCard.GetComponent<Button>().onClick.AddListener(() => self.OnClickCard(handCard, card));
                    self.RefreshHandCard(handCard, card);
                }
                for (int i = cards.Count; i < parent.childCount; i++)
                {
                    parent.GetChild(i).gameObject.SetActive(false);
                }
            }

            private static void RefreshHandCard(this UIRoomPlayer self, GameObject item, HandCard card)
            {
                if (self.SelectCards.Contains(card))
                {
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.up * 50;
                }
                else
                {
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }

            private static void RefreshPlayCards(this UIRoomPlayer self, List<HandCard> cards)
            {
                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(RoomResourceType.PlayCard.StringToAB(), RoomResourceType.PlayCard);
                Transform parent = self.PlayCards.transform;
                for (int i = 0; i < cards.Count; i++)
                {
                    HandCard card = cards[i];
                    GameObject handCard = null;
                    if (i >= parent.childCount)
                    {
                        handCard = GameObject.Instantiate<GameObject>(bundleGameObject, parent);
                    }
                    else
                    {
                        handCard = parent.GetChild(i).gameObject;
                        handCard.SetActive(true);
                    }
                    handCard.GetComponent<Image>().sprite = uiRoomComponent.GetSprite(card.ToString());
                }
                for (int i = cards.Count; i < parent.childCount; i++)
                {
                    parent.GetChild(i).gameObject.SetActive(false);
                }
            }

            private static void RefreshPrompt(this UIRoomPlayer self, ELandlordStatus status)
            {
                switch (status)
                {
                    case ELandlordStatus.None:
                        self.Prompt.text = string.Empty;
                        break;
                    case ELandlordStatus.Ready:
                        self.Prompt.text = "准备";
                        break;
                    case ELandlordStatus.NotCall:
                        self.Prompt.text = "不叫";
                        break;
                    case ELandlordStatus.CallLandlord:
                        self.Prompt.text = "叫地主";
                        break;
                    case ELandlordStatus.DontRob:
                        self.Prompt.text = "不抢";
                        break;
                    case ELandlordStatus.RobLandlord:
                        self.Prompt.text = "抢地主";
                        break;
                    case ELandlordStatus.ShowCards:
                        self.Prompt.text = string.Empty;
                        break;
                    case ELandlordStatus.DontShow:
                        self.Prompt.text = "不出";
                        break;
                }
            }

            public static void RefreshTimer(this UIRoomPlayer self, LandlordTimeoutComponent timeoutComponent)
            {
                self.Timeout = timeoutComponent;
                if (timeoutComponent != null)
                {
                    self.Timer.SetActive(true);
                    self.UpdateTimer();
                }
                else
                {
                    self.Timer.SetActive(false);
                }
            }

            public static void UpdateTimer(this UIRoomPlayer self)
            {
                if (self.Timeout == null) return;
                self.Time.text = self.Timeout.GetTime().ToString();
            }

            private static void OnClickCard(this UIRoomPlayer self, GameObject item, HandCard card)
            {
                bool isSelect = !self.SelectCards.Contains(card);
                if (isSelect)
                {
                    self.SelectCards.Add(card);
                }
                else
                {
                    self.SelectCards.Remove(card);
                }
                self.RefreshHandCard(item, card);
            }
        }
    }
}
