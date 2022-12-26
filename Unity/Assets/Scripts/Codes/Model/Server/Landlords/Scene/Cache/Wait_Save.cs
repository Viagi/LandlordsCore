using ET.Landlords;

namespace ET.Server
{
    namespace Landlords
    {
        public struct Wait_Save : IWaitType
        {
            public int Error { get; set; }
            public ChangeUserRequest Message { get; set; }
            public ETTask<bool> Task { get; set; }
        }
    }
}