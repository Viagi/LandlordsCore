using System;
using ETModel;
using Google.Protobuf;

namespace ETHotfix
{
    public class OuterMessageDispatcher : IMessageDispatcher
    {
        public async void Dispatch(Session session, ushort opcode, object message)
        {
            try
            {
                switch (message)
                {
                    case IActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
                            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);

                            int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                            IResponse response = await actorLocationSender.Call(actorLocationRequest);
                            response.RpcId = rpcId;

                            session.Reply(response);
                            return;
                        }
                    case IActorLocationMessage actorLocationMessage:
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
                            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(actorId);
                            actorLocationSender.Send(actorLocationMessage);
                            return;
                        }
                    case IActorRequest iActorRequest:
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
                            ActorMessageSender actorSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(actorId);

                            int rpcId = iActorRequest.RpcId; // 这里要保存客户端的rpcId
                            IResponse response = await actorSender.Call(iActorRequest);
                            response.RpcId = rpcId;

                            session.Reply(response);
                            return;
                        }
                    case IActorMessage iactorMessage:
                        {
                            long actorId = session.GetComponent<SessionUserComponent>().User.ActorID;
                            ActorMessageSender actorSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(actorId);
                            actorSender.Send(iactorMessage);
                            return;
                        }
                }

                Game.Scene.GetComponent<MessageDispatherComponent>().Handle(session, new MessageInfo(opcode, message));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
