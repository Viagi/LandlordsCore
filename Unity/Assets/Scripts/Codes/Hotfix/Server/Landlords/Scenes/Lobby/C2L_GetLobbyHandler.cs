using ET.Landlords;
using System;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [ActorMessageHandler(SceneType.Lobby)]
        public class C2L_GetLobbyHandler : AMActorRpcHandler<LobbyUnitEntity, C2L_GetLobby, L2C_GetLobby>
        {
            protected override async ETTask Run(LobbyUnitEntity unit, C2L_GetLobby request, L2C_GetLobby response, Action reply)
            {
                List<Entity> components = await UserHelper.AccessUserComponents(unit.Id);
                if (components != null)
                {
                    response.Components = new List<byte[]>();
                    foreach (Entity component in components)
                    {
                        response.Components.Add(MongoHelper.Serialize(component));
                    }
                }

                reply();
            }
        }
    }
}