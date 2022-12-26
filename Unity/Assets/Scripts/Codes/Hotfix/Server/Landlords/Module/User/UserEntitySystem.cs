namespace ET.Server
{
    namespace Landlords
    {
        [FriendOf(typeof(UserEntity))]
        public static class UserEntitySystem
        {
            public static async ETTask SaveDB(this UserEntity self)
            {
                using (CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.User, self.Id))
                {
                    if(self.Saver != null)
                    {
                        self.Saver.Cancel();
                        self.Saver = null;
                    }
                    Scene scene = self.DomainScene();
                    DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();
                    DBComponent dbComponent = dbManagerComponent.GetZoneDB(scene.Zone);
                    await dbComponent.Save(self);
                }
            }

            public static void DirtySaveDB(this UserEntity self)
            {
                self.InnerDirtySaveDB().Coroutine();
            }

            private static async ETTask InnerDirtySaveDB(this UserEntity self)
            {
                if (self.Saver == null)
                {
                    self.Saver = new ETCancellationToken();
                    await TimerComponent.Instance.WaitAsync(RandomGenerator.RandomNumber(0, 1000), self.Saver);
                    self.Saver = null;
                    await self.SaveDB();
                }
            }
        }
    }
}
