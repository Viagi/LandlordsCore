using System;
using Model;

namespace Hotfix
{
	public class OuterMessageDispatcher: IMessageDispatcher
	{
		public async void Dispatch(Session session, ushort opcode, int offset, byte[] messageBytes, AMessage message)
		{
			// gate session收到actor消息直接转发给actor自己去处理
			if (message is AActorMessage)
			{
				long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
				ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(actorId);
				actorProxy.Send(message);
				return;
			}

			// gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
			if (message is AActorRequest aActorRequest)
			{
                long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
                ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(actorId);
                uint rpcId = aActorRequest.RpcId;
				AResponse response = await actorProxy.Call<AResponse>(aActorRequest);
				response.RpcId = rpcId;
				session.Reply(response);
				return;
			}

			if (message != null)
			{
				Game.Scene.GetComponent<MessageDispatherComponent>().Handle(session, message);
				return;
			}

			throw new Exception($"message type error: {message.GetType().FullName}");
		}
	}
}
