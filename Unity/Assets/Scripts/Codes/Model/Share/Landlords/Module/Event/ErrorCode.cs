namespace ET
{
    [UniqueId(100000, 500000)]
    public static partial class ErrorCode
    {
        public const int ERR_MyErrorCode = 111000;

        // 111000 以上，避免跟ET错误码冲突


        //-----------------------------------

        // 小于这个Rpc会抛异常，大于这个异常的error需要自己判断处理，也就是说需要处理的错误应该要大于该值
        public const int ERR_MyException = 201000;

        //账号或密码不正确
        public const int ERR_AccountOrPasswordError = 201001;
        //账号已存在
        public const int ERR_AccountAlreadyExist = 201002;

        //账号已存在
        public const int ERR_MoneyNotEnough = 201003;

        public const int ERR_RobotConnectFailed = 202003;
    }
}