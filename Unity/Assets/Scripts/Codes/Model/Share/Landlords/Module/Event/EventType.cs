using ET.Landlords;
using System.Collections.Generic;

namespace ET
{
    namespace EventType
    {
        namespace Landlords
        {
            public struct AppStartInitFinish
            {
            }

            public struct LoginFinish
            {
            }

            public struct StartMatch
            {

            }

            public struct EnterRoom
            {

            }

            public struct ReturnLobby
            {

            }

            public struct PlayerEnter
            {
                public List<long> Units;
            }

            public struct PlayerExit
            {
                public long UnitId;
            }

            public struct PlayerReconnect
            {
                public List<long> Units;
            }

            public struct PlayerDisconnect
            {
                public long UnitId;
            }

            public struct PlayerReady
            {
                public long UnitId;
            }

            public struct PlayerTimeout
            {
                public RoomUnitEntity Unit;
            }

            public struct GameStart
            {

            }

            public struct SetLandlord
            {
                public long UnitId;
                public List<HandCard> Cards;
            }

            public struct RobLandlord
            {
                public long UnitId;
            }

            public struct PlayerSwitch
            {
            }

            public struct PlayCards
            {
                public long UnitId;
                public List<HandCard> Cards;
            }

            public struct GameEnd
            {
                public List<long> Results;
            }

            public struct PlayerTrust
            {
                public long UnitId;
            }
        }
    }
}
