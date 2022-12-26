using ET.Landlords;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class EndComponentAwakeSystem : AwakeSystem<EndComponent>
        {
            protected override void Awake(EndComponent self)
            {
                self.Root = self.GetParent<UIRoomComponent>().Root.Get<GameObject>("End");
                ReferenceCollector rc = self.Root.GetComponent<ReferenceCollector>();
                self.Win = rc.Get<GameObject>("Win");
                self.Lose = rc.Get<GameObject>("Lose");
                self.GamerContent = rc.Get<GameObject>("GamerContent");
                self.ContinueBtn = rc.Get<GameObject>("ContinueButton").GetComponent<Button>();

                self.Awake();
            }
        }

        [FriendOf(typeof(EndComponent))]
        public static class EndComponentSystem
        {
            public static void Awake(this EndComponent self)
            {
                self.ContinueBtn.onClick.RemoveAllListeners();
                self.ContinueBtn.onClick.AddListener(self.OnContinue);
                self.Root.SetActive(false);
            }

            public static void Show(this EndComponent self, long winner, List<long> results)
            {
                RoomComponent roomComponent = self.DomainScene().GetComponent<RoomComponent>();
                UIRoomComponent uiRoomComponent = self.GetParent<UIRoomComponent>();
                RoomEntity room = roomComponent.Room;
                RoomUnitEntity myUnit = roomComponent.GetMyUnit();

                self.Root.SetActive(true);
                self.Win.SetActive(winner == myUnit.Id);
                self.Lose.SetActive(winner != myUnit.Id);

                GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(RoomResourceType.Content.StringToAB(), RoomResourceType.Content);
                Transform parent = self.GamerContent.transform;
                for (int i = 0; i < room.Seats.Count; i++)
                {
                    RoomUnitEntity unit = room.Get(i);
                    AccountComponent accountComponent = unit.GetComponent<AccountComponent>();
                    GameObject content = null;

                    if (i >= parent.childCount)
                        content = GameObject.Instantiate<GameObject>(bundleGameObject, parent);
                    else
                        content = parent.GetChild(i).gameObject;

                    content.Get<GameObject>("NickName").GetComponent<Text>().text = accountComponent.NickName;
                    content.Get<GameObject>("BaseScore").GetComponent<Text>().text = room.BaseMoney.ToString();
                    content.Get<GameObject>("Multiples").GetComponent<Text>().text = (room.BaseRate * room.Rate).ToString();
                    content.Get<GameObject>("Score").GetComponent<Text>().text = results[i].ToString();

                    if (unit.Identity == ELandlordIdentity.Landlord)
                        content.Get<GameObject>("Identity").GetComponent<Image>().sprite = uiRoomComponent.GetSprite("Identity_Landlord");
                    else
                        content.Get<GameObject>("Identity").GetComponent<Image>().sprite = uiRoomComponent.GetSprite("Identity_Farmer");
                }
            }

            private static void OnContinue(this EndComponent self)
            {
                self.Root.SetActive(false);
            }
        }
    }
}
