namespace ET.Server
{
    namespace Landlords
    {
        public static class SessionHelper
        {
            public static Player GetPlayer(this Session self)
            {
                SessionPlayerComponent sessionPlayerComponent = self.GetComponent<SessionPlayerComponent>();
                Player player = null;

                if (sessionPlayerComponent != null)
                {
                    player = sessionPlayerComponent.GetPlayer();
                }

                return player;
            }
        }
    }
}
