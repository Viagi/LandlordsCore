using ET.EventType.Landlords;
using ET.Landlords;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UIRoomComponentAwakeSystem : AwakeSystem<UIRoomComponent>
        {
            protected override void Awake(UIRoomComponent self)
            {
                self.Root = self.GetParent<UI>().GameObject;
                ReferenceCollector rc = self.Root.GetComponent<ReferenceCollector>();
                self.MatchPrompt = rc.Get<GameObject>("MatchPrompt");
                self.Multiples = rc.Get<GameObject>("Multiples").GetComponent<Text>();
                self.QuitBtn = rc.Get<GameObject>("QuitButton").GetComponent<Button>();
                self.ReadyBtn = rc.Get<GameObject>("ReadyButton").GetComponent<Button>();

                self.Desk = rc.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
                self.LandPokers = new List<Image>();
                self.LandPokers.Add(self.Desk.Get<GameObject>("Poker0").GetComponent<Image>());
                self.LandPokers.Add(self.Desk.Get<GameObject>("Poker1").GetComponent<Image>());
                self.LandPokers.Add(self.Desk.Get<GameObject>("Poker2").GetComponent<Image>());

                GameObject gamers = rc.Get<GameObject>("Gamers");
                self.AddChildWithId<UIRoomPlayer, GameObject>(0, gamers.Get<GameObject>("Local"));
                self.AddChildWithId<UIRoomPlayer, GameObject>(1, gamers.Get<GameObject>("Left"));
                self.AddChildWithId<UIRoomPlayer, GameObject>(2, gamers.Get<GameObject>("Right"));

                GameObject atlas = (GameObject)ResourcesComponent.Instance.GetAsset(RoomResourceType.Atlas.StringToAB(), RoomResourceType.Atlas);
                self.Atlas = atlas.GetComponent<ReferenceCollector>();

                self.Awake();
            }
        }

        [FriendOf(typeof(UIRoomComponent))]
        public static class UIRoomComponentSystem
        {
            public static void Awake(this UIRoomComponent self)
            {
                self.QuitBtn.onClick.RemoveAllListeners();
                self.QuitBtn.onClick.AddListener(self.OnQuit);
                self.ReadyBtn.onClick.RemoveAllListeners();
                self.ReadyBtn.onClick.AddListener(self.OnReady);
                self.ReadyBtn.gameObject.SetActive(false);
                self.Multiples.gameObject.SetActive(false);
                self.Desk.SetActive(false);
            }

            public static void Init(this UIRoomComponent self, RoomComponent roomComponent)
            {
                RoomEntity room = roomComponent.Room;

                foreach (RoomUnitEntity unit in room.Children.Values)
                {
                    self.Add(unit.Id);
                }

                self.MatchPrompt.SetActive(false);
                self.RefreshReady(roomComponent);
                self.RefreshLandlordCards(roomComponent);
                self.RefreshRate(roomComponent);
            }

            public static void Add(this UIRoomComponent self, long unitId)
            {
                if (self.Players.ContainsKey(unitId))
                {
                    return;
                }

                RoomComponent roomComponent = self.DomainScene().GetComponent<RoomComponent>();
                RoomEntity room = roomComponent.Room;
                RoomUnitEntity myUnit = roomComponent.GetMyUnit();
                RoomUnitEntity newUnit = room.GetChild<RoomUnitEntity>(unitId);

                UIRoomPlayer player;
                if (newUnit == myUnit)
                {
                    //客户端玩家
                    player = self.GetChild<UIRoomPlayer>(0);
                }
                else if ((newUnit.Index + 1) % 3 == myUnit.Index)
                {
                    //顺时针右边玩家
                    player = self.GetChild<UIRoomPlayer>(2);
                }
                else
                {
                    //顺时针左边玩家
                    player = self.GetChild<UIRoomPlayer>(1);
                }
                player.Init(room, newUnit, newUnit == myUnit);
                self.Players.Add(newUnit.Id, player);
            }

            public static void Remove(this UIRoomComponent self, long unitId)
            {
                UIRoomPlayer player = self.Get(unitId);
                if (player != null)
                {
                    player.Reset();
                    self.Players.Remove(unitId);
                }
            }

            public static UIRoomPlayer Get(this UIRoomComponent self, long unitId)
            {
                UIRoomPlayer player;
                self.Players.TryGetValue(unitId, out player);

                return player;
            }

            public static UIRoomPlayer GetMyPlayer(this UIRoomComponent self)
            {
                RoomComponent roomComponent = self.DomainScene().GetComponent<RoomComponent>();
                return self.Get(roomComponent.MyId);
            }

            public static Sprite GetSprite(this UIRoomComponent self, string name = "None")
            {
                return self.Atlas.Get<Sprite>(name);
            }

            public static void RefreshReady(this UIRoomComponent self, RoomComponent roomComponent)
            {
                RoomEntity room = roomComponent.Room;
                AccountComponent accountComponent = self.DomainScene().GetComponent<AccountComponent>();
                self.ReadyBtn.gameObject.SetActive(room.Status == ERoomStatus.None && accountComponent.Money > 0);
            }

            public static void RefreshLandlordCards(this UIRoomComponent self, RoomComponent roomComponent)
            {
                RoomEntity room = roomComponent.Room;
                if (room.Status == ERoomStatus.PlayCard)
                {
                    for (int i = 0; i < self.LandPokers.Count; i++)
                    {
                        self.LandPokers[i].sprite = self.GetSprite(room.LandlordCards[i].ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < self.LandPokers.Count; i++)
                    {
                        self.LandPokers[i].sprite = self.GetSprite();
                    }
                }
                self.Desk.SetActive(room.Status != ERoomStatus.None);
            }

            public static void RefreshRate(this UIRoomComponent self, RoomComponent roomComponent)
            {
                RoomEntity room = roomComponent.Room;
                self.Multiples.gameObject.SetActive(room.Status != ERoomStatus.None);
                self.Multiples.text = room.Rate.ToString();
            }

            private static void OnQuit(this UIRoomComponent self)
            {
                self.InnerQuit().Coroutine();
            }

            private static async ETTask InnerQuit(this UIRoomComponent self)
            {
                Scene scene = self.DomainScene();
                scene.GetComponent<ObjectWait>().Notify(new Wait_JoinRoom() { Error = WaitTypeError.Cancel });
                await LobbyHelper.ReturnLobby(scene);
                EventSystem.Instance.Publish(scene, new ReturnLobby());
            }

            private static void OnReady(this UIRoomComponent self)
            {
                self.ReadyBtn.gameObject.SetActive(false);
                RoomHelper.Ready(self.DomainScene());
            }
        }
    }
}
