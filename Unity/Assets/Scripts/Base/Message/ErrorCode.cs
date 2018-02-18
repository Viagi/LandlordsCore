namespace Model
{
	public static partial class ErrorCode
	{
		// ����ڲ�����
		public const int ERR_Success = 0;
		public const int ERR_NotFoundActor = 2;

		public const int ERR_RpcFail = 10001;
		public const int ERR_SocketDisconnected = 10002;
		public const int ERR_ReloadFail = 10003;
		public const int ERR_ActorLocationNotFound = 10004;

		// �߼���ش���
		public const int ERR_AccountOrPasswordError = 20002;
		public const int ERR_ConnectGateKeyError = 20003;
		public const int ERR_NotFoundUnit = 20004;
		public const int ERR_SessionActorError = 20007;
		
	}
}