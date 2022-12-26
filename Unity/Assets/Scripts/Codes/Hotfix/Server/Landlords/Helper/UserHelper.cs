using ET.Landlords;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

namespace ET.Server
{
    namespace Landlords
    {
        public static class UserHelper
        {
            public static async ETTask<T> AccessUserComponent<T>(long userId, bool readWrite = false) where T : Entity
            {
                List<Entity> components = await AccessUserComponents(userId, readWrite);
                if (components != null)
                {
                    foreach (Entity component in components)
                    {
                        if (component is T)
                        {
                            return component as T;
                        }
                    }
                }

                return null;
            }

            public static async ETTask<List<Entity>> AccessUserComponents(long userId, bool readWrite = false)
            {
                AccessUserResponse accessResponse = (AccessUserResponse)await CacheHelper.Call(new AccessUserRequest() { UserId = userId, ReadWrite = readWrite });

                if (accessResponse.Components != null)
                {
                    List<Entity> components = new List<Entity>();
                    foreach (byte[] bytes in accessResponse.Components)
                    {
                        components.Add(MongoHelper.Deserialize<Entity>(bytes));
                    }
                    return components;
                }
                else
                {
                    return null;
                }
            }

            public static async ETTask ChangeUserComponent(long userId, Entity component, bool save = false)
            {
                await ChangeUserComponents(userId, new List<Entity>() { component }, save);
            }

            public static async ETTask ChangeUserComponents(long userId, List<Entity> components, bool save = false)
            {
                List<byte[]> entitys = new List<byte[]>();
                foreach (Entity component in components)
                {
                    entitys.Add(MongoHelper.Serialize(component));
                }
                await CacheHelper.Call(new ChangeUserRequest() { UserId = userId, Components = entitys, Save = save });
            }
        }
    }
}
