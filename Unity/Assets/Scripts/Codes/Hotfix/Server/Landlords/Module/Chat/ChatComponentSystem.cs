using ET.Landlords;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(ChatComponent))]
        public static class ChatComponentSystem
        {
            public static async ETTask<SortedList<long, ChatEntity>> Query(this ChatComponent self, long sender, long receiver)
            {
                SortedList<long, ChatEntity> entitys = self.Get(sender, receiver);
                if (entitys == null)
                {
                    //查询数据库
                    Scene scene = self.DomainScene();
                    DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                    DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                    List<ChatEntity> results = await dbComponent.QueryJson<ChatEntity>($"{{\"Sender\":\"{sender}\",\"Receiver\":\"{receiver}\"}}");
                    if (results != null)
                    {
                        foreach (ChatEntity entity in results)
                        {
                            entitys = self.Add(entity);
                        }
                    }
                    results = await dbComponent.QueryJson<ChatEntity>($"{{\"Sender\":\"{receiver}\",\"Receiver\":\"{sender}\"}}");
                    if (results != null)
                    {
                        foreach (ChatEntity entity in results)
                        {
                            entitys = self.Add(entity);
                        }
                    }
                }

                return entitys;
            }

            public static async ETTask<ChatEntity> Create(this ChatComponent self, long sender, long receiver, string content)
            {
                Scene scene = self.DomainScene();
                DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                ChatEntity entity = self.AddChild<ChatEntity, string, long, long>(content, sender, receiver);

                self.Add(entity);
                await dbComponent.Save(entity);

                return entity;
            }
        }
    }
}
