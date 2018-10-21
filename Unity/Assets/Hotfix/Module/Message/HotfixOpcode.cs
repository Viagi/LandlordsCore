using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

	[Message(HotfixOpcode.C2R_Login_Req)]
	public partial class C2R_Login_Req : IRequest {}

	[Message(HotfixOpcode.R2C_Login_Ack)]
	public partial class R2C_Login_Ack : IResponse {}

	[Message(HotfixOpcode.C2R_Register_Req)]
	public partial class C2R_Register_Req : IRequest {}

	[Message(HotfixOpcode.R2C_Register_Ack)]
	public partial class R2C_Register_Ack : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate_Req)]
	public partial class C2G_LoginGate_Req : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate_Ack)]
	public partial class G2C_LoginGate_Ack : IResponse {}

	[Message(HotfixOpcode.C2G_GetUserInfo_Req)]
	public partial class C2G_GetUserInfo_Req : IRequest {}

	[Message(HotfixOpcode.G2C_GetUserInfo_Ack)]
	public partial class G2C_GetUserInfo_Ack : IResponse {}

	[Message(HotfixOpcode.C2G_StartMatch_Req)]
	public partial class C2G_StartMatch_Req : IRequest {}

	[Message(HotfixOpcode.G2C_StartMatch_Ack)]
	public partial class G2C_StartMatch_Ack : IResponse {}

	[Message(HotfixOpcode.C2G_ReturnLobby_Ntt)]
	public partial class C2G_ReturnLobby_Ntt : IMessage {}

	[Message(HotfixOpcode.Actor_GamerReady_Ntt)]
	public partial class Actor_GamerReady_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_GamerGrabLandlordSelect_Ntt)]
	public partial class Actor_GamerGrabLandlordSelect_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Req)]
	public partial class Actor_GamerPlayCard_Req : IActorRequest {}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ack)]
	public partial class Actor_GamerPlayCard_Ack : IActorResponse {}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ntt)]
	public partial class Actor_GamerPlayCard_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_GamerPrompt_Req)]
	public partial class Actor_GamerPrompt_Req : IActorRequest {}

	[Message(HotfixOpcode.Actor_GamerPrompt_Ack)]
	public partial class Actor_GamerPrompt_Ack : IActorResponse {}

	[Message(HotfixOpcode.Actor_GamerDontPlay_Ntt)]
	public partial class Actor_GamerDontPlay_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_Trusteeship_Ntt)]
	public partial class Actor_Trusteeship_Ntt : IActorMessage {}

	[Message(HotfixOpcode.GamerInfo)]
	public partial class GamerInfo {}

	[Message(HotfixOpcode.Actor_GamerEnterRoom_Ntt)]
	public partial class Actor_GamerEnterRoom_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_GamerExitRoom_Ntt)]
	public partial class Actor_GamerExitRoom_Ntt : IActorMessage {}

//抢地主状态
	[Message(HotfixOpcode.GamerState)]
	public partial class GamerState {}

	[Message(HotfixOpcode.Actor_GamerReconnect_Ntt)]
	public partial class Actor_GamerReconnect_Ntt : IActorMessage {}

	[Message(HotfixOpcode.GamerCardNum)]
	public partial class GamerCardNum : IMessage {}

	[Message(HotfixOpcode.Actor_GameStart_Ntt)]
	public partial class Actor_GameStart_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_AuthorityGrabLandlord_Ntt)]
	public partial class Actor_AuthorityGrabLandlord_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_AuthorityPlayCard_Ntt)]
	public partial class Actor_AuthorityPlayCard_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_SetMultiples_Ntt)]
	public partial class Actor_SetMultiples_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_SetLandlord_Ntt)]
	public partial class Actor_SetLandlord_Ntt : IActorMessage {}

	[Message(HotfixOpcode.GamerScore)]
	public partial class GamerScore {}

	[Message(HotfixOpcode.Actor_Gameover_Ntt)]
	public partial class Actor_Gameover_Ntt : IActorMessage {}

	[Message(HotfixOpcode.Actor_GamerMoneyLess_Ntt)]
	public partial class Actor_GamerMoneyLess_Ntt : IActorMessage {}

}
namespace ETHotfix
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
		 public const ushort PlayerInfo = 10008;
		 public const ushort C2G_PlayerInfo = 10009;
		 public const ushort G2C_PlayerInfo = 10010;
		 public const ushort C2R_Login_Req = 10011;
		 public const ushort R2C_Login_Ack = 10012;
		 public const ushort C2R_Register_Req = 10013;
		 public const ushort R2C_Register_Ack = 10014;
		 public const ushort C2G_LoginGate_Req = 10015;
		 public const ushort G2C_LoginGate_Ack = 10016;
		 public const ushort C2G_GetUserInfo_Req = 10017;
		 public const ushort G2C_GetUserInfo_Ack = 10018;
		 public const ushort C2G_StartMatch_Req = 10019;
		 public const ushort G2C_StartMatch_Ack = 10020;
		 public const ushort C2G_ReturnLobby_Ntt = 10021;
		 public const ushort Actor_GamerReady_Ntt = 10022;
		 public const ushort Actor_GamerGrabLandlordSelect_Ntt = 10023;
		 public const ushort Actor_GamerPlayCard_Req = 10024;
		 public const ushort Actor_GamerPlayCard_Ack = 10025;
		 public const ushort Actor_GamerPlayCard_Ntt = 10026;
		 public const ushort Actor_GamerPrompt_Req = 10027;
		 public const ushort Actor_GamerPrompt_Ack = 10028;
		 public const ushort Actor_GamerDontPlay_Ntt = 10029;
		 public const ushort Actor_Trusteeship_Ntt = 10030;
		 public const ushort GamerInfo = 10031;
		 public const ushort Actor_GamerEnterRoom_Ntt = 10032;
		 public const ushort Actor_GamerExitRoom_Ntt = 10033;
		 public const ushort GamerState = 10034;
		 public const ushort Actor_GamerReconnect_Ntt = 10035;
		 public const ushort GamerCardNum = 10036;
		 public const ushort Actor_GameStart_Ntt = 10037;
		 public const ushort Actor_AuthorityGrabLandlord_Ntt = 10038;
		 public const ushort Actor_AuthorityPlayCard_Ntt = 10039;
		 public const ushort Actor_SetMultiples_Ntt = 10040;
		 public const ushort Actor_SetLandlord_Ntt = 10041;
		 public const ushort GamerScore = 10042;
		 public const ushort Actor_Gameover_Ntt = 10043;
		 public const ushort Actor_GamerMoneyLess_Ntt = 10044;
	}
}
