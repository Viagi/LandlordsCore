using ET;using ProtoBuf;using System.Collections.Generic;
namespace ET.Landlords
{
	[ResponseType("ET.Landlords." + nameof(R2C_Login))]
	[Message(OuterMessage.C2R_Login)]
	[ProtoContract]
	public partial class C2R_Login: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.R2C_Login)]
	[ProtoContract]
	public partial class R2C_Login: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string Address { get; set; }

		[ProtoMember(5)]
		public long Key { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(R2C_Register))]
	[Message(OuterMessage.C2R_Register)]
	[ProtoContract]
	public partial class C2R_Register: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(OuterMessage.R2C_Register)]
	[ProtoContract]
	public partial class R2C_Register: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public string Address { get; set; }

		[ProtoMember(5)]
		public long Key { get; set; }

	}

	[Message(OuterMessage.ClientOffline)]
	[ProtoContract]
	public partial class ClientOffline: ProtoObject, IMessage
	{
	}

	[ResponseType("ET.Landlords." + nameof(G2C_LoginGate))]
	[Message(OuterMessage.C2G_LoginGate)]
	[ProtoContract]
	public partial class C2G_LoginGate: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long Key { get; set; }

	}

	[Message(OuterMessage.G2C_LoginGate)]
	[ProtoContract]
	public partial class G2C_LoginGate: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(L2C_GetLobby))]
	[Message(OuterMessage.C2L_GetLobby)]
	[ProtoContract]
	public partial class C2L_GetLobby: ProtoObject, IActorLobbyRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.L2C_GetLobby)]
	[ProtoContract]
	public partial class L2C_GetLobby: ProtoObject, IActorLobbyResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public List<byte[]> Components { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(F2C_GetFriends))]
	[Message(OuterMessage.C2F_GetFriends)]
	[ProtoContract]
	public partial class C2F_GetFriends: ProtoObject, IActorFriendRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.F2C_GetFriends)]
	[ProtoContract]
	public partial class F2C_GetFriends: ProtoObject, IActorFriendResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public byte[] Entity { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(G2C_EnterMatch))]
	[Message(OuterMessage.C2G_EnterMatch)]
	[ProtoContract]
	public partial class C2G_EnterMatch: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_EnterMatch)]
	[ProtoContract]
	public partial class G2C_EnterMatch: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.G2C_EnterRoom)]
	[ProtoContract]
	public partial class G2C_EnterRoom: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public byte[] Entity { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(G2C_ReturnLobby))]
	[Message(OuterMessage.C2G_ReturnLobby)]
	[ProtoContract]
	public partial class C2G_ReturnLobby: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(OuterMessage.G2C_ReturnLobby)]
	[ProtoContract]
	public partial class G2C_ReturnLobby: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.Actor_PlayerEnter)]
	[ProtoContract]
	public partial class Actor_PlayerEnter: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> Entitys { get; set; }

	}

	[Message(OuterMessage.Actor_PlayerExit)]
	[ProtoContract]
	public partial class Actor_PlayerExit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

	}

	[Message(OuterMessage.Actor_PlayerReady)]
	[ProtoContract]
	public partial class Actor_PlayerReady: ProtoObject, IActorRoomMessage
	{
		[ProtoMember(2)]
		public long UnitId { get; set; }

	}

	[Message(OuterMessage.Actor_GameStart)]
	[ProtoContract]
	public partial class Actor_GameStart: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> Entitys { get; set; }

	}

	[Message(OuterMessage.Actor_PlaySwitch)]
	[ProtoContract]
	public partial class Actor_PlaySwitch: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public ERoomStatus Status { get; set; }

		[ProtoMember(2)]
		public int Before { get; set; }

		[ProtoMember(3)]
		public int Current { get; set; }

		[ProtoMember(4)]
		public int Active { get; set; }

		[ProtoMember(5)]
		public long Rate { get; set; }

		[ProtoMember(6)]
		public long Time { get; set; }

	}

	[Message(OuterMessage.Actor_CallLandlord)]
	[ProtoContract]
	public partial class Actor_CallLandlord: ProtoObject, IActorRoomMessage
	{
		[ProtoMember(1)]
		public bool CallLandlord { get; set; }

		[ProtoMember(2)]
		public int Index { get; set; }

	}

	[Message(OuterMessage.Actor_RobLandlord)]
	[ProtoContract]
	public partial class Actor_RobLandlord: ProtoObject, IActorRoomMessage
	{
		[ProtoMember(1)]
		public bool RobLandlord { get; set; }

		[ProtoMember(2)]
		public int Index { get; set; }

	}

	[Message(OuterMessage.Actor_SetLandlord)]
	[ProtoContract]
	public partial class Actor_SetLandlord: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int Index { get; set; }

		[ProtoMember(2)]
		public List<HandCard> Cards { get; set; }

	}

	[Message(OuterMessage.Actor_PlayCards)]
	[ProtoContract]
	public partial class Actor_PlayCards: ProtoObject, IActorRoomMessage
	{
		[ProtoMember(1)]
		public List<HandCard> Cards { get; set; }

		[ProtoMember(2)]
		public bool ShowCards { get; set; }

		[ProtoMember(3)]
		public int Index { get; set; }

	}

	[Message(OuterMessage.Actor_GameEnd)]
	[ProtoContract]
	public partial class Actor_GameEnd: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> Entitys { get; set; }

		[ProtoMember(2)]
		public List<long> Results { get; set; }

		[ProtoMember(3)]
		public byte[] Component { get; set; }

	}

	[Message(OuterMessage.Actor_Trust)]
	[ProtoContract]
	public partial class Actor_Trust: ProtoObject, IActorRoomMessage
	{
		[ProtoMember(1)]
		public bool IsTrust { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(G2C_ConnectGate))]
	[Message(OuterMessage.C2G_ConnectGate)]
	[ProtoContract]
	public partial class C2G_ConnectGate: ProtoObject, IRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(4)]
		public long ActorId { get; set; }

	}

	[Message(OuterMessage.G2C_ConnectGate)]
	[ProtoContract]
	public partial class G2C_ConnectGate: ProtoObject, IResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(OuterMessage.Actor_PlayerDisconnect)]
	[ProtoContract]
	public partial class Actor_PlayerDisconnect: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UnitId { get; set; }

	}

	[Message(OuterMessage.Actor_PlayerReconnect)]
	[ProtoContract]
	public partial class Actor_PlayerReconnect: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public List<byte[]> Entitys { get; set; }

	}

	public static class OuterMessage
	{
		 public const ushort C2R_Login = 10102;
		 public const ushort R2C_Login = 10103;
		 public const ushort C2R_Register = 10104;
		 public const ushort R2C_Register = 10105;
		 public const ushort ClientOffline = 10106;
		 public const ushort C2G_LoginGate = 10107;
		 public const ushort G2C_LoginGate = 10108;
		 public const ushort C2L_GetLobby = 10109;
		 public const ushort L2C_GetLobby = 10110;
		 public const ushort C2F_GetFriends = 10111;
		 public const ushort F2C_GetFriends = 10112;
		 public const ushort C2G_EnterMatch = 10113;
		 public const ushort G2C_EnterMatch = 10114;
		 public const ushort G2C_EnterRoom = 10115;
		 public const ushort C2G_ReturnLobby = 10116;
		 public const ushort G2C_ReturnLobby = 10117;
		 public const ushort Actor_PlayerEnter = 10118;
		 public const ushort Actor_PlayerExit = 10119;
		 public const ushort Actor_PlayerReady = 10120;
		 public const ushort Actor_GameStart = 10121;
		 public const ushort Actor_PlaySwitch = 10122;
		 public const ushort Actor_CallLandlord = 10123;
		 public const ushort Actor_RobLandlord = 10124;
		 public const ushort Actor_SetLandlord = 10125;
		 public const ushort Actor_PlayCards = 10126;
		 public const ushort Actor_GameEnd = 10127;
		 public const ushort Actor_Trust = 10128;
		 public const ushort C2G_ConnectGate = 10129;
		 public const ushort G2C_ConnectGate = 10130;
		 public const ushort Actor_PlayerDisconnect = 10131;
		 public const ushort Actor_PlayerReconnect = 10132;
	}
}
