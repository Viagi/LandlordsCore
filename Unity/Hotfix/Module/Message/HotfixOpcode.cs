namespace Model
{
	public static partial class HotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort G2C_TestHotfixMessage = 10005;
		 public const ushort C2M_TestActorRequest = 10006;
		 public const ushort M2C_TestActorResponse = 10007;
		 public const ushort C2R_Login_Req = 10008;
		 public const ushort R2C_Login_Ack = 10009;
		 public const ushort C2R_Register_Req = 10010;
		 public const ushort R2C_Register_Ack = 10011;
		 public const ushort C2G_LoginGate_Req = 10012;
		 public const ushort G2C_LoginGate_Ack = 10013;
		 public const ushort C2G_GetUserInfo_Req = 10014;
		 public const ushort G2C_GetUserInfo_Ack = 10015;
		 public const ushort C2G_StartMatch_Req = 10016;
		 public const ushort G2C_StartMatch_Ack = 10017;
		 public const ushort C2G_ReturnLobby_Ntt = 10018;
		 public const ushort Actor_GamerReady_Ntt = 10019;
		 public const ushort Actor_GamerGrabLandlordSelect_Ntt = 10020;
		 public const ushort Actor_GamerPlayCard_Req = 10021;
		 public const ushort Actor_GamerPlayCard_Ack = 10022;
		 public const ushort Actor_GamerPlayCard_Ntt = 10023;
		 public const ushort Actor_GamerPrompt_Req = 10024;
		 public const ushort Actor_GamerPrompt_Ack = 10025;
		 public const ushort Actor_GamerDontPlay_Ntt = 10026;
		 public const ushort Actor_Trusteeship_Ntt = 10027;
		 public const ushort G2C_PlayerDisconnect_Ntt = 10028;
		 public const ushort GamerInfo = 10029;
		 public const ushort Actor_GamerEnterRoom_Ntt = 10030;
		 public const ushort Actor_GamerExitRoom_Ntt = 10031;
		 public const ushort Actor_GamerReconnect_Ntt = 10032;
		 public const ushort Actor_GameStart_Ntt = 10033;
		 public const ushort Actor_AuthorityGrabLandlord_Ntt = 10034;
		 public const ushort Actor_AuthorityPlayCard_Ntt = 10035;
		 public const ushort Actor_SetMultiples_Ntt = 10036;
		 public const ushort Actor_SetLandlord_Ntt = 10037;
		 public const ushort Actor_Gameover_Ntt = 10038;
		 public const ushort Actor_GamerMoneyLess_Ntt = 10039;
	}
}
