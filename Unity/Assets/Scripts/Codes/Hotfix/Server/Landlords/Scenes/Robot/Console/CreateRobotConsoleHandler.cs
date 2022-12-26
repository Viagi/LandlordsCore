using System;
using System.Collections.Generic;
using CommandLine;

namespace ET.Server
{
    namespace Landlords
    {
        [ConsoleHandler(ET.Landlords.ConsoleMode.CreateRobot)]
        public class CreateRobotConsoleHandler : IConsoleHandler
        {
            public async ETTask Run(ModeContex contex, string content)
            {
                switch (content)
                {
                    case ConsoleMode.CreateRobot:
                        Log.Console("CreateRobot args error!");
                        break;
                    default:
                        CreateRobotArgs options = null;
                        Parser.Default.ParseArguments<CreateRobotArgs>(content.Split(' '))
                                .WithNotParsed(error => throw new Exception($"CreateRobotArgs error!"))
                                .WithParsed(o => { options = o; });

                        // 创建机器人
                        for (int i = 0; i < options.Num; ++i)
                        {
                            StartSceneConfig robotSceneConfig = AddressHelper.GetRobot();
                            Scene robotScene = ServerSceneManagerComponent.Instance.Get(robotSceneConfig.Id);
                            RobotManagerComponent robotManagerComponent = robotScene.GetComponent<RobotManagerComponent>();
                            Scene robot = await robotManagerComponent.NewRobot(Options.Instance.Process * 10000 + i);
                            robot.AddComponent<AIComponent, int>(3);
                            Log.Console($"create robot {robot.Zone}");
                            await TimerComponent.Instance.WaitAsync(2000);
                        }
                        break;
                }
                contex.Parent.RemoveComponent<ModeContex>();
                await ETTask.CompletedTask;
            }
        }
    }
}