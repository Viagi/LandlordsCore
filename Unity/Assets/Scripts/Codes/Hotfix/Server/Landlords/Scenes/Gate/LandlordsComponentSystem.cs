using ET.Landlords;
using System.ServiceModel.Channels;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class LandlordsComponentAwakeSystem : AwakeSystem<LandlordsComponent, long, bool>
        {
            protected override void Awake(LandlordsComponent self, long userId, bool isRobot)
            {
                self.UserId = userId;
                self.IsRobot = isRobot;
            }
        }

        [ObjectSystem]
        public class LandlordsComponentDestroySystem : DestroySystem<LandlordsComponent>
        {
            protected override void Destroy(LandlordsComponent self)
            {
                if(Root.Instance == null || self.IsRobot)
                {
                    return;
                }

                CacheHelper.Send(new Actor_RemoveUnit() { UserId = self.UserId, UnitId = self.Parent.Id, SceneType = (int)SceneType.Gate });

                Actor_Offline message = new Actor_Offline();
                if (self.LobbyId != 0)
                {
                    MessageHelper.SendActor(self.LobbyId, message);
                }
                if (self.FriendId != 0)
                {
                    MessageHelper.SendActor(self.FriendId, message);
                }
                if (self.MatchId != 0)
                {
                    MessageHelper.SendActor(self.MatchId, message);
                }
                if (self.RoomId != 0)
                {
                    MessageHelper.SendActor(self.RoomId, message);
                }
            }
        }
    }
}
