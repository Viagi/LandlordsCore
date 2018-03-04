using ProtoBuf;
using Model;
using Hotfix;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Hotfix
{
	#region ET
	[Message(HotfixOpcode.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: IRequest
	{
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
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
	public partial class C2M_TestActorRequest: MessageObject, IActorRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	[ProtoContract]
	public partial class M2C_TestActorResponse: MessageObject, IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public string Info;

	}

	#endregion
	#region Client-Realm
	[Message(HotfixOpcode.C2R_Login_Req)]
	[ProtoContract]
	public partial class C2R_Login_Req: IRequest
	{
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
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	#endregion
	#region Client-Gate
	[Message(HotfixOpcode.C2G_LoginGate_Req)]
	[ProtoContract]
	public partial class C2G_LoginGate_Req: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(HotfixOpcode.G2C_LoginGate_Ack)]
	[ProtoContract]
	public partial class G2C_LoginGate_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.G2C_GetUserInfo_Ack)]
	[ProtoContract]
	public partial class G2C_GetUserInfo_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
	}

	[Message(HotfixOpcode.G2C_StartMatch_Ack)]
	[ProtoContract]
	public partial class G2C_StartMatch_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
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
	public partial class Actor_GamerReady_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_GamerGrabLandlordSelect_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerGrabLandlordSelect_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool IsGrab;

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Req)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Req: MessageObject, IActorRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ack)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Ack: MessageObject, IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(HotfixOpcode.Actor_GamerPlayCard_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerPlayCard_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerPrompt_Req)]
	[ProtoContract]
	public partial class Actor_GamerPrompt_Req: MessageObject, IActorRequest
	{
	}

	[Message(HotfixOpcode.Actor_GamerPrompt_Ack)]
	[ProtoContract]
	public partial class Actor_GamerPrompt_Ack: MessageObject, IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public Card[] Cards;

	}

	[Message(HotfixOpcode.Actor_GamerDontPlay_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerDontPlay_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_Trusteeship_Ntt)]
	[ProtoContract]
	public partial class Actor_Trusteeship_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool isTrusteeship;

	}

	#endregion
	#region Gate-Client
	[Message(HotfixOpcode.G2C_PlayerDisconnect_Ntt)]
	[ProtoContract]
	public partial class G2C_PlayerDisconnect_Ntt: IMessage
	{
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
	public partial class Actor_GamerEnterRoom_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<GamerInfo> Gamers = new List<GamerInfo>();

	}

	[Message(HotfixOpcode.Actor_GamerExitRoom_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerExitRoom_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_GamerReconnect_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerReconnect_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public int Multiples;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(2)]
		public Dictionary<long,Identity> GamersIdentity = new Dictionary<long,Identity>();

		[ProtoMember(3, IsRequired = true)]
		public Card[] LordCards;

		[ProtoMember(4, IsRequired = true)]
		public KeyValuePair<long,Card[]> DeskCards;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(5)]
		public Dictionary<long,bool> GamerGrabLandlordState = new Dictionary<long,bool>();

	}

	[Message(HotfixOpcode.Actor_GameStart_Ntt)]
	[ProtoContract]
	public partial class Actor_GameStart_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public Card[] GamerCards;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(2)]
		public Dictionary<long,int> GamerCardsNum = new Dictionary<long,int>();

	}

	[Message(HotfixOpcode.Actor_AuthorityGrabLandlord_Ntt)]
	[ProtoContract]
	public partial class Actor_AuthorityGrabLandlord_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(HotfixOpcode.Actor_AuthorityPlayCard_Ntt)]
	[ProtoContract]
	public partial class Actor_AuthorityPlayCard_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public bool IsFirst;

	}

	[Message(HotfixOpcode.Actor_SetMultiples_Ntt)]
	[ProtoContract]
	public partial class Actor_SetMultiples_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public int Multiples;

	}

	[Message(HotfixOpcode.Actor_SetLandlord_Ntt)]
	[ProtoContract]
	public partial class Actor_SetLandlord_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public Card[] LordCards;

	}

	[Message(HotfixOpcode.Actor_Gameover_Ntt)]
	[ProtoContract]
	public partial class Actor_Gameover_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public Identity Winner;

		[ProtoMember(2, IsRequired = true)]
		public long BasePointPerMatch;

		[ProtoMember(3, IsRequired = true)]
		public int Multiples;

		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		[ProtoMember(4)]
		public Dictionary<long,long> GamersScore = new Dictionary<long,long>();

	}

	[Message(HotfixOpcode.Actor_GamerMoneyLess_Ntt)]
	[ProtoContract]
	public partial class Actor_GamerMoneyLess_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	#endregion
}
#if SERVER
namespace Model
{
	[BsonKnownTypes(typeof(C2M_TestActorRequest))]
	[BsonKnownTypes(typeof(M2C_TestActorResponse))]
	[BsonKnownTypes(typeof(Actor_GamerReady_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerGrabLandlordSelect_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerPlayCard_Req))]
	[BsonKnownTypes(typeof(Actor_GamerPlayCard_Ack))]
	[BsonKnownTypes(typeof(Actor_GamerPlayCard_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerPrompt_Req))]
	[BsonKnownTypes(typeof(Actor_GamerPrompt_Ack))]
	[BsonKnownTypes(typeof(Actor_GamerDontPlay_Ntt))]
	[BsonKnownTypes(typeof(Actor_Trusteeship_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerEnterRoom_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerExitRoom_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerReconnect_Ntt))]
	[BsonKnownTypes(typeof(Actor_GameStart_Ntt))]
	[BsonKnownTypes(typeof(Actor_AuthorityGrabLandlord_Ntt))]
	[BsonKnownTypes(typeof(Actor_AuthorityPlayCard_Ntt))]
	[BsonKnownTypes(typeof(Actor_SetMultiples_Ntt))]
	[BsonKnownTypes(typeof(Actor_SetLandlord_Ntt))]
	[BsonKnownTypes(typeof(Actor_Gameover_Ntt))]
	[BsonKnownTypes(typeof(Actor_GamerMoneyLess_Ntt))]
	public partial class MessageObject {}

}
#endif
namespace Model
{
	public partial class MessageObject {}

}
