using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class AllotMapComponentStartSystem : StartSystem<AllotMapComponent>
    {
        public override void Start(AllotMapComponent self)
        {
            self.Start();
        }
    }

    public static class AllotMapComponentSystem
    {
        public static void Start(this AllotMapComponent self)
        {
            StartConfig[] startConfigs = self.GetParent<Entity>().GetComponent<StartConfigComponent>().GetAll();
            foreach (StartConfig config in startConfigs)
            {
                if (!config.AppType.Is(AppType.Map))
                {
                    continue;
                }

                self.MapAddress.Add(config);
            }
        }

        /// <summary>
        /// 随机获取一个房间服务器地址
        /// </summary>
        /// <returns></returns>
        public static StartConfig GetAddress(this AllotMapComponent self)
        {
            int n = RandomHelper.RandomNumber(0, self.MapAddress.Count);
            return self.MapAddress[n];
        }
    }
}
