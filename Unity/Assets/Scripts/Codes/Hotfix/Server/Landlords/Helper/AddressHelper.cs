using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        public static class AddressHelper
        {
            public static StartSceneConfig GetLobby(int zone)
            {
                List<StartSceneConfig> zoneLobbys = StartSceneConfigCategory.Instance.Lobbys[zone];

                int n = RandomGenerator.RandomNumber(0, zoneLobbys.Count);

                return zoneLobbys[n];
            }

            public static StartSceneConfig GetFriend(int zone)
            {
                List<StartSceneConfig> zoneFriends = StartSceneConfigCategory.Instance.Friends[zone];

                int n = RandomGenerator.RandomNumber(0, zoneFriends.Count);

                return zoneFriends[n];
            }

            public static StartSceneConfig GetRoom(int zone)
            {
                List<StartSceneConfig> zoneRooms = StartSceneConfigCategory.Instance.Rooms[zone];

                int n = RandomGenerator.RandomNumber(0, zoneRooms.Count);

                return zoneRooms[n];
            }

            public static StartSceneConfig GetRobot()
            {
                using (ListComponent<StartSceneConfig> thisProcessRobotScenes = ListComponent<StartSceneConfig>.Create())
                {
                    foreach (StartSceneConfig robotSceneConfig in StartSceneConfigCategory.Instance.Robots)
                    {
                        if (robotSceneConfig.Process != Options.Instance.Process)
                        {
                            continue;
                        }
                        thisProcessRobotScenes.Add(robotSceneConfig);
                    }

                    int n = RandomGenerator.RandomNumber(0, thisProcessRobotScenes.Count);

                    return thisProcessRobotScenes[n];
                }
            }
        }
    }
}
