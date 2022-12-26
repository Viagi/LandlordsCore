using System;
using System.IO;
using UnityEngine;

namespace ET.Client
{
    namespace Landlords
    {
        [ObjectSystem]
        public class ClientConsoleComponentAwakeSystem : AwakeSystem<ClientConsoleComponent>
        {
            protected override void Awake(ClientConsoleComponent self)
            {
                self.Default = Console.In;
            }
        }

        [ObjectSystem]
        public class ClientConsoleComponentDestroySystem : DestroySystem<ClientConsoleComponent>
        {
            protected override void Destroy(ClientConsoleComponent self)
            {
                self.Reset();
            }
        }

        [ObjectSystem]
        public class ClientConsoleComponentUpdateSystem : UpdateSystem<ClientConsoleComponent>
        {
            protected override void Update(ClientConsoleComponent self)
            {
                self.Update();
            }
        }

        [FriendOf(typeof(ClientConsoleComponent))]
        public static class ClientConsoleComponentSystem
        {
            public static void Update(this ClientConsoleComponent self)
            {
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    self.Reset();
                    self.Commond = new StringReader($"{ET.Landlords.ConsoleMode.CreateRobot} --Num=2");
                    Console.SetIn(self.Commond);
                }
            }

            public static void Reset(this ClientConsoleComponent self)
            {
                if (self.Commond != null)
                {
                    self.Commond.Close();
                    self.Commond = null;
                    Console.SetIn(self.Default);
                }
            }
        }
    }
}
