namespace ETModel
{
	public static partial class InnerOpcode
	{
		 public const ushort M2M_TrasferUnitRequest = 1001;
		 public const ushort M2M_TrasferUnitResponse = 1002;
		 public const ushort M2A_Reload = 1003;
		 public const ushort A2M_Reload = 1004;
		 public const ushort G2G_LockRequest = 1005;
		 public const ushort G2G_LockResponse = 1006;
		 public const ushort G2G_LockReleaseRequest = 1007;
		 public const ushort G2G_LockReleaseResponse = 1008;
		 public const ushort DBSaveRequest = 1009;
		 public const ushort DBSaveBatchResponse = 1010;
		 public const ushort DBSaveBatchRequest = 1011;
		 public const ushort DBSaveResponse = 1012;
		 public const ushort DBQueryRequest = 1013;
		 public const ushort DBQueryResponse = 1014;
		 public const ushort DBQueryBatchRequest = 1015;
		 public const ushort DBQueryBatchResponse = 1016;
		 public const ushort DBQueryJsonRequest = 1017;
		 public const ushort DBQueryJsonResponse = 1018;
		 public const ushort ObjectAddRequest = 1019;
		 public const ushort ObjectAddResponse = 1020;
		 public const ushort ObjectRemoveRequest = 1021;
		 public const ushort ObjectRemoveResponse = 1022;
		 public const ushort ObjectLockRequest = 1023;
		 public const ushort ObjectLockResponse = 1024;
		 public const ushort ObjectUnLockRequest = 1025;
		 public const ushort ObjectUnLockResponse = 1026;
		 public const ushort ObjectGetRequest = 1027;
		 public const ushort ObjectGetResponse = 1028;
		 public const ushort R2G_GetLoginKey = 1029;
		 public const ushort G2R_GetLoginKey = 1030;
		 public const ushort G2M_CreateUnit = 1031;
		 public const ushort M2G_CreateUnit = 1032;
		 public const ushort G2M_SessionDisconnect = 1033;
		 public const ushort G2R_PlayerOnline_Req = 1034;
		 public const ushort R2G_PlayerOnline_Ack = 1035;
		 public const ushort G2R_PlayerOffline_Req = 1036;
		 public const ushort R2G_PlayerOffline_Ack = 1037;
		 public const ushort R2G_GetLoginKey_Req = 1038;
		 public const ushort G2R_GetLoginKey_Ack = 1039;
		 public const ushort R2G_PlayerKickOut_Req = 1040;
		 public const ushort G2R_PlayerKickOut_Ack = 1041;
		 public const ushort G2M_PlayerEnterMatch_Req = 1042;
		 public const ushort M2G_PlayerEnterMatch_Ack = 1043;
		 public const ushort G2M_PlayerExitMatch_Req = 1044;
		 public const ushort M2G_PlayerExitMatch_Ack = 1045;
		 public const ushort Actor_PlayerExitRoom_Req = 1046;
		 public const ushort Actor_PlayerExitRoom_Ack = 1047;
		 public const ushort Actor_MatchSucess_Ntt = 1048;
		 public const ushort MH2MP_CreateRoom_Req = 1049;
		 public const ushort MP2MH_CreateRoom_Ack = 1050;
		 public const ushort Actor_PlayerEnterRoom_Req = 1051;
		 public const ushort Actor_PlayerEnterRoom_Ack = 1052;
		 public const ushort MP2MH_PlayerExitRoom_Req = 1053;
		 public const ushort MH2MP_PlayerExitRoom_Ack = 1054;
		 public const ushort MP2MH_SyncRoomState_Ntt = 1055;
	}
}
