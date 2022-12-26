using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ET.Server
{
    namespace Landlords
    {
        [ObjectSystem]
        public class UserManagerComponentAwakeSystem : AwakeSystem<UserManagerComponent>
        {
            protected override void Awake(UserManagerComponent self)
            {
                //预热数据库
                Scene scene = self.DomainScene();
                DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                dbComponent.Preheat<UserEntity>();
            }
        }

        [FriendOf(typeof(UserManagerComponent))]
        public static class UserManagerComponentSystem
        {
            public static async ETTask<UserEntity> Get(this UserManagerComponent self, string account, string password)
            {
                long userId = 0;
                UserEntity entity = null;

                //查询缓存
                if (!string.IsNullOrEmpty(password))
                {
                    KeyValuePair<string, string> accountPair = new(account, password);
                    if (self.AccountPasswordDict.TryGetValue(accountPair, out userId))
                    {
                        entity = self.Users[userId];
                    }
                }
                else
                {
                    if (self.AccountDict.TryGetValue(account, out userId))
                    {
                        entity = self.Users[userId];
                    }
                }

                if (entity == null)
                {
                    //查询数据库
                    Scene scene = self.DomainScene();
                    DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                    DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                    List<UserEntity> results = null;

                    Stopwatch time = new Stopwatch();
                    time.Start();
                    if (!string.IsNullOrEmpty(password))
                    {
                        results = await dbComponent.QueryJson<UserEntity>($"{{\"Account\":\"{account}\",\"Password\":\"{password}\"}}");
                    }
                    else
                    {
                        results = await dbComponent.QueryJson<UserEntity>($"{{\"Account\":\"{account}\"}}");
                    }
                    time.Stop();
                    Log.Debug($"查询账号耗时:{time.ElapsedMilliseconds} ms");

                    if (results.Count > 0)
                    {
                        //查询结果缓存
                        entity = results[0];
                        self.AddChild(entity);
                        self.Add(entity);
                    }
                }

                return entity;
            }

            public static void Add(this UserManagerComponent self, UserEntity entity)
            {
                entity.AddComponent<ObjectWait>();
                self.Users.Add(entity.Id, entity);
                if (!string.IsNullOrEmpty(entity.Account) && !string.IsNullOrEmpty(entity.Password))
                {
                    self.AccountPasswordDict.Add(new KeyValuePair<string, string>(entity.Account, entity.Password), entity.Id);
                    self.AccountDict.Add(entity.Account, entity.Id);
                }
            }

            public static void Remove(this UserManagerComponent self, UserEntity entity)
            {
                self.AccountPasswordDict.Remove(new KeyValuePair<string, string>(entity.Account, entity.Password));
                self.Users.Remove(entity.Id);
            }

            public static async ETTask<UserEntity> Get(this UserManagerComponent self, long userId)
            {
                UserEntity entity;
                self.Users.TryGetValue(userId, out entity);
                if (entity == null)
                {
                    Scene scene = self.DomainScene();
                    DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                    DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                    entity = await dbComponent.Query<UserEntity>(userId);
                    self.AddChild(entity);
                    self.Add(entity);
                }

                return entity;
            }
        }
    }
}
