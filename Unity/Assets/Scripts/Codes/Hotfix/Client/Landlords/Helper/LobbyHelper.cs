using ET.Landlords;

namespace ET.Client
{
    namespace Landlords
    {
        public static class LobbyHelper
        {
            public static async ETTask<int> EnterMatch(Scene clientScene, ETCancellationToken cancelToken = null)
            {
                G2C_EnterMatch g2C_EnterMatch = (G2C_EnterMatch)await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMatch(), cancelToken);

                if (cancelToken == null || !cancelToken.IsCancel())
                {
                    return g2C_EnterMatch.Error;
                }
                else
                {
                    return 0;
                }
            }

            public static async ETTask ReturnLobby(Scene clientScene, ETCancellationToken cancelToken = null)
            {
                await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_ReturnLobby(), cancelToken);
                clientScene.RemoveComponent<RoomComponent>();
            }

            public static async ETTask GetLobby(Scene clientScene, ETCancellationToken cancelToken = null)
            {
                L2C_GetLobby l2C_GetLobby = (L2C_GetLobby)await clientScene.GetComponent<SessionComponent>().Session.Call(new C2L_GetLobby(), cancelToken);
                if (cancelToken == null || !cancelToken.IsCancel())
                {
                    foreach (byte[] bytes in l2C_GetLobby.Components)
                    {
                        Entity component = MongoHelper.Deserialize<Entity>(bytes);
                        clientScene.RemoveComponent(component.GetType());
                        clientScene.AddComponent(component);
                    }
                }
            }
        }
    }
}
