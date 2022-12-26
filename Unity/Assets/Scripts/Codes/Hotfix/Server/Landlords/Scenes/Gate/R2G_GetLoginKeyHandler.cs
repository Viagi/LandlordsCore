using System;


namespace ET.Server
{
	namespace Landlords
	{
		[ActorMessageHandler(SceneType.Gate)]
		public class R2G_GetLoginKeyHandler : AMActorRpcHandler<Scene, ET.Landlords.R2G_GetLoginKey, ET.Landlords.G2R_GetLoginKey>
		{
			protected override async ETTask Run(Scene scene, ET.Landlords.R2G_GetLoginKey request, ET.Landlords.G2R_GetLoginKey response, Action reply)
			{
				long key = RandomGenerator.RandInt64();
				scene.GetComponent<GateSessionKeyComponent>().Add(key, request.UserId.ToString());
				response.Key = key;
				response.GateId = scene.InstanceId;
				reply();
				await ETTask.CompletedTask;
			}
		}
	}
}