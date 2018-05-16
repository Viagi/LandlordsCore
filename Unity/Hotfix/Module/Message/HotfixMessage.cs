using ProtoBuf;
using ETModel;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
namespace ETHotfix
{
	#region ET

	[Message(HotfixOpcode.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Account;

		[ProtoMember(2, IsRequired = true)]
		public string Password;

	}

	[Message(HotfixOpcode.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Address;

		[ProtoMember(2, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long PlayerId;

	}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	[ProtoContract]
	public partial class G2C_TestHotfixMessage: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	[ProtoContract]
	public partial class C2M_TestActorRequest: IActorRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	[ProtoContract]
	public partial class M2C_TestActorResponse: IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.PlayerInfo)]
	[ProtoContract]
	public partial class PlayerInfo: IMessage
	{
	}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	[ProtoContract]
	public partial class C2G_PlayerInfo: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

	}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	[ProtoContract]
	public partial class G2C_PlayerInfo: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, TypeName = "ETHotfix.PlayerInfo")]
		public List<PlayerInfo> PlayerInfos = new List<PlayerInfo>();

	}

	#endregion

	#region Client-Realm

	[Message(HotfixOpcode.C2R_Login_Req)]
	[ProtoContract]
	public partial class C2R_Login_Req: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Account;

		[ProtoMember(2, IsRequired = true)]
		public string Password;

	}

	[Message(HotfixOpcode.R2C_Login_Ack)]
	[ProtoContract]
	public partial class R2C_Login_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

		[ProtoMember(2, IsRequired = true)]
		public string Address;

	}

	[Message(HotfixOpcode.C2R_Register_Req)]
	[ProtoContract]
	public partial class C2R_Register_Req: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Account;

		[ProtoMember(2, IsRequired = true)]
		public string Password;

	}

	[Message(HotfixOpcode.R2C_Register_Ack)]
	[ProtoContract]
	public partial class R2C_Register_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

	}

	#endregion

	#region Client-Gate

	[Message(HotfixOpcode.C2G_LoginGate_Req)]
	[ProtoContract]
	public partial class C2G_LoginGate_Req: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.G2C_LoginGate_Ack)]
	[ProtoContract]
	public partial class G2C_LoginGate_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long PlayerID;

		[ProtoMember(2, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.C2G_GetUserInfo_Req)]
	[ProtoContract]
	public partial class C2G_GetUserInfo_Req: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.G2C_GetUserInfo_Ack)]
	[ProtoContract]
	public partial class G2C_GetUserInfo_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string NickName;

		[ProtoMember(2, IsRequired = true)]
		public int Wins;

		[ProtoMember(3, IsRequired = true)]
		public int Loses;

		[ProtoMember(4, IsRequired = true)]
		public long Money;

	}

	[Message(HotfixOpcode.C2G_StartMatch_Req)]
	[ProtoContract]
	public partial class C2G_StartMatch_Req: IRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

	}

	[Message(HotfixOpcode.G2C_StartMatch_Ack)]
	[ProtoContract]
	public partial class G2C_StartMatch_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(HotfixOpcode.C2G_ReturnLobby_Ntt)]
	[ProtoContract]
	public partial class C2G_ReturnLobby_Ntt: IMessage
	{
	}

	#endregion

	#region Client-Map

	[Message(HotfixOpcode.Actor_GamerReady_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerReady_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_GamerGrabLandlordSelect_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerGrabLandlordSelect_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool IsGrab;

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Req)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Req: IActorRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ack)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Ack: IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerPrompt_Req)]
	[ProtoContract]
	public partial class Actor_GamerPrompt_Req: IActorRequest
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

	}

	[Message(HotfixOpcode.Actor_GamerPrompt_Ack)]
	[ProtoContract]
	public partial class Actor_GamerPrompt_Ack: IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(92, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerDontPlay_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerDontPlay_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_Trusteeship_Ntt)]
	[ProtoContract]
	public partial class Actor_Trusteeship_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool isTrusteeship;

	}

	#endregion

	#region Map-Client

	[Message(HotfixOpcode.GamerInfo)]
	[ProtoContract]
	public partial class GamerInfo
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool IsReady;

	}

	[Message(HotfixOpcode.Actor_GamerEnterRoom_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerEnterRoom_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, TypeName = "ETHotfix.GamerInfo")]
		public List<GamerInfo> Gamers = new List<GamerInfo>();

	}

	[Message(HotfixOpcode.Actor_GamerExitRoom_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerExitRoom_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.GamerState)]
	[ProtoContract]
	public partial class GamerState
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public byte Identity;

		[ProtoMember(3, IsRequired = true)]
		public bool GrabLandlordState;

	}

	[Message(HotfixOpcode.Actor_GamerReconnect_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerReconnect_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public int Multiples;

		[ProtoMember(2, TypeName = "ETHotfix.GamerState")]
		public List<GamerState> GamersState = new List<GamerState>();

		[ProtoMember(3, IsRequired = true)]
		public Card[] LordCards;

		[ProtoMember(4, IsRequired = true)]
		public KeyValuePair<long,Card[]> DeskCards;

	}

	[Message(HotfixOpcode.GamerCardNum)]
	[ProtoContract]
	public partial class GamerCardNum
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public int Num;

	}

	[Message(HotfixOpcode.Actor_GameStart_Ntt)]
	[ProtoContract]
	public partial class Actor_GameStart_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public Card[] HandCards;

		[ProtoMember(2, TypeName = "ETHotfix.GamerCardNum")]
		public List<GamerCardNum> GamersCardNum = new List<GamerCardNum>();

	}

	[Message(HotfixOpcode.Actor_AuthorityGrabLandlord_Ntt)]
	[ProtoContract]
	public partial class Actor_AuthorityGrabLandlord_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_AuthorityPlayCard_Ntt)]
	[ProtoContract]
	public partial class Actor_AuthorityPlayCard_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool IsFirst;

	}

	[Message(HotfixOpcode.Actor_SetMultiples_Ntt)]
	[ProtoContract]
	public partial class Actor_SetMultiples_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public int Multiples;

	}

	[Message(HotfixOpcode.Actor_SetLandlord_Ntt)]
	[ProtoContract]
	public partial class Actor_SetLandlord_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public Card[] LordCards;

	}

	[Message(HotfixOpcode.GamerScore)]
	[ProtoContract]
	public partial class GamerScore
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public long Score;

	}

	[Message(HotfixOpcode.Actor_Gameover_Ntt)]
	[ProtoContract]
	public partial class Actor_Gameover_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public byte Winner;

		[ProtoMember(2, IsRequired = true)]
		public long BasePointPerMatch;

		[ProtoMember(3, IsRequired = true)]
		public int Multiples;

		[ProtoMember(4, TypeName = "ETHotfix.GamerScore")]
		public List<GamerScore> GamersScore = new List<GamerScore>();

	}

	[Message(HotfixOpcode.Actor_GamerMoneyLess_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerMoneyLess_Ntt: IActorMessage
	{
		[ProtoMember(90, IsRequired = true)]
		public int RpcId { get; set; }

		[ProtoMember(93, IsRequired = true)]
		public long ActorId { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	#endregion

}
