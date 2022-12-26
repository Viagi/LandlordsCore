namespace ET.Server
{
    namespace Landlords
    {
        [ComponentOf]
        public class UnitRobotComponent : Entity, IAwake, IDestroy
        {
            public Scene Robot { get; set; }
        }
    }
}
