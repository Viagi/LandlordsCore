using System.Reflection;
using System;
using MongoDB.Driver;

namespace ET.Server
{
    namespace Landlords
    {
        [ConsoleHandler(ConsoleMode.DB)]
        public class DBConsoleHandler : IConsoleHandler
        {
            public async ETTask Run(ModeContex contex, string content)
            {
                string[] ss = content.Split(" ");
                switch (ss[0])
                {
                    case ConsoleMode.DB:
                        break;

                    case "Drop":
                        int zone = int.Parse(ss[1]);
                        await InnerDropDB(StartZoneConfigCategory.Instance.Get(zone));
                        break;
                    case "DropAll":
                        foreach (var config in StartZoneConfigCategory.Instance.GetAll().Values)
                        {
                            await InnerDropDB(config);
                        }
                        break;
                }
                await ETTask.CompletedTask;
            }

            private async ETTask InnerDropDB(StartZoneConfig config)
            {
                try
                {
                    if(config == null)
                    {
                        Log.Debug($"drop {config.Id} zone database error:\nStartZoneConfig not found");
                        MongoClient client = new MongoClient(config.DBConnection);
                        Log.Debug($"drop {config.Id} zone database start");
                        await client.DropDatabaseAsync(config.DBName);
                        Log.Debug($"drop {config.Id} zone database end");
                        return;
                    }
                }
                catch (Exception e)
                {
                    Log.Debug($"drop {config.Id} zone database error:\n{e}");
                }
            }
        }
    }
}
