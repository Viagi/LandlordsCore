using ET;using ProtoBuf;using System.Collections.Generic;
namespace ET.Landlords
{
	[ResponseType("ET.Landlords." + nameof(C2R_GetAccount))]
	[Message(InnerMessage.R2C_GetAccount)]
	[ProtoContract]
	public partial class R2C_GetAccount: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(InnerMessage.C2R_GetAccount)]
	[ProtoContract]
	public partial class C2R_GetAccount: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UserId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(C2R_CreateAccount))]
	[Message(InnerMessage.R2C_CreateAccount)]
	[ProtoContract]
	public partial class R2C_CreateAccount: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public string Account { get; set; }

		[ProtoMember(3)]
		public string Password { get; set; }

	}

	[Message(InnerMessage.C2R_CreateAccount)]
	[ProtoContract]
	public partial class C2R_CreateAccount: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UserId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(C2R_CreateOnlineUnit))]
	[Message(InnerMessage.R2C_CreateOnlineUnit)]
	[ProtoContract]
	public partial class R2C_CreateOnlineUnit: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public long GateId { get; set; }

		[ProtoMember(4)]
		public long GateKey { get; set; }

	}

	[Message(InnerMessage.C2R_CreateOnlineUnit)]
	[ProtoContract]
	public partial class C2R_CreateOnlineUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(Actor_GetOnlineUnitResponse))]
	[Message(InnerMessage.Actor_GetOnlineUnitRequest)]
	[ProtoContract]
	public partial class Actor_GetOnlineUnitRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

	}

	[Message(InnerMessage.Actor_GetOnlineUnitResponse)]
	[ProtoContract]
	public partial class Actor_GetOnlineUnitResponse: ProtoObject, IActorResponse
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

	[Message(InnerMessage.Actor_RemoveOnlineUnit)]
	[ProtoContract]
	public partial class Actor_RemoveOnlineUnit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UserId { get; set; }

		[ProtoMember(2)]
		public long GateId { get; set; }

		[ProtoMember(3)]
		public long GateKey { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(G2R_GetLoginKey))]
	[Message(InnerMessage.R2G_GetLoginKey)]
	[ProtoContract]
	public partial class R2G_GetLoginKey: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

	}

	[Message(InnerMessage.G2R_GetLoginKey)]
	[ProtoContract]
	public partial class G2R_GetLoginKey: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long Key { get; set; }

		[ProtoMember(5)]
		public long GateId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(L2G_LoginLobby))]
	[Message(InnerMessage.G2L_LoginLobby)]
	[ProtoContract]
	public partial class G2L_LoginLobby: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public byte[] Entity { get; set; }

	}

	[Message(InnerMessage.L2G_LoginLobby)]
	[ProtoContract]
	public partial class L2G_LoginLobby: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UnitId { get; set; }

	}

	[Message(InnerMessage.Actor_Offline)]
	[ProtoContract]
	public partial class Actor_Offline: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(AccessUserResponse))]
	[Message(InnerMessage.AccessUserRequest)]
	[ProtoContract]
	public partial class AccessUserRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public bool ReadWrite { get; set; }

	}

	[Message(InnerMessage.AccessUserResponse)]
	[ProtoContract]
	public partial class AccessUserResponse: ProtoObject, IActorResponse
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

	[ResponseType("ET.Landlords." + nameof(ChangeUserResponse))]
	[Message(InnerMessage.ChangeUserRequest)]
	[ProtoContract]
	public partial class ChangeUserRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public List<byte[]> Components { get; set; }

		[ProtoMember(4)]
		public bool Save { get; set; }

	}

	[Message(InnerMessage.ChangeUserResponse)]
	[ProtoContract]
	public partial class ChangeUserResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(F2G_LoginFriend))]
	[Message(InnerMessage.G2F_LoginFriend)]
	[ProtoContract]
	public partial class G2F_LoginFriend: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public byte[] Entity { get; set; }

	}

	[Message(InnerMessage.F2G_LoginFriend)]
	[ProtoContract]
	public partial class F2G_LoginFriend: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UnitId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(M2G_CreateMatchUnit))]
	[Message(InnerMessage.G2M_CreateMatchUnit)]
	[ProtoContract]
	public partial class G2M_CreateMatchUnit: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UserId { get; set; }

		[ProtoMember(3)]
		public byte[] Entity { get; set; }

	}

	[Message(InnerMessage.M2G_CreateMatchUnit)]
	[ProtoContract]
	public partial class M2G_CreateMatchUnit: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UnitId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(Actor_RemoveMatchUnitResponse))]
	[Message(InnerMessage.Actor_RemoveMatchUnitRequest)]
	[ProtoContract]
	public partial class Actor_RemoveMatchUnitRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.Actor_RemoveMatchUnitResponse)]
	[ProtoContract]
	public partial class Actor_RemoveMatchUnitResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(R2M_GetRoom))]
	[Message(InnerMessage.M2R_GetRoom)]
	[ProtoContract]
	public partial class M2R_GetRoom: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.R2M_GetRoom)]
	[ProtoContract]
	public partial class R2M_GetRoom: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.Actor_RoomStart)]
	[ProtoContract]
	public partial class Actor_RoomStart: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long RoomId { get; set; }

	}

	[Message(InnerMessage.Actor_RoomEnd)]
	[ProtoContract]
	public partial class Actor_RoomEnd: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long RoomId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(Actor_JoinRoomResponse))]
	[Message(InnerMessage.Actor_JoinRoomRequest)]
	[ProtoContract]
	public partial class Actor_JoinRoomRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(3)]
		public List<long> UserIds { get; set; }

		[ProtoMember(4)]
		public List<byte[]> Entitys { get; set; }

	}

	[Message(InnerMessage.Actor_JoinRoomResponse)]
	[ProtoContract]
	public partial class Actor_JoinRoomResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(Actor_ExitRoomResponse))]
	[Message(InnerMessage.Actor_ExitRoomRequest)]
	[ProtoContract]
	public partial class Actor_ExitRoomRequest: ProtoObject, IActorLocationRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

	}

	[Message(InnerMessage.Actor_ExitRoomResponse)]
	[ProtoContract]
	public partial class Actor_ExitRoomResponse: ProtoObject, IActorLocationResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

	}

	[Message(InnerMessage.Actor_JoinRoomMessage)]
	[ProtoContract]
	public partial class Actor_JoinRoomMessage: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public byte[] Entity { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

	}

	[Message(InnerMessage.Actor_CreateUnit)]
	[ProtoContract]
	public partial class Actor_CreateUnit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UserId { get; set; }

		[ProtoMember(2)]
		public int SceneType { get; set; }

		[ProtoMember(3)]
		public long UnitId { get; set; }

	}

	[Message(InnerMessage.Actor_RemoveUnit)]
	[ProtoContract]
	public partial class Actor_RemoveUnit: ProtoObject, IActorMessage
	{
		[ProtoMember(1)]
		public long UserId { get; set; }

		[ProtoMember(2)]
		public int SceneType { get; set; }

		[ProtoMember(3)]
		public long UnitId { get; set; }

	}

	[ResponseType("ET.Landlords." + nameof(Actor_ConnectRobotResponse))]
	[Message(InnerMessage.Actor_ConnectRobotRequest)]
	[ProtoContract]
	public partial class Actor_ConnectRobotRequest: ProtoObject, IActorRequest
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public long UnitId { get; set; }

		[ProtoMember(3)]
		public byte[] Entity { get; set; }

	}

	[Message(InnerMessage.Actor_ConnectRobotResponse)]
	[ProtoContract]
	public partial class Actor_ConnectRobotResponse: ProtoObject, IActorResponse
	{
		[ProtoMember(1)]
		public int RpcId { get; set; }

		[ProtoMember(2)]
		public int Error { get; set; }

		[ProtoMember(3)]
		public string Message { get; set; }

		[ProtoMember(4)]
		public long UserId { get; set; }

	}

	public static class InnerMessage
	{
		 public const ushort R2C_GetAccount = 20102;
		 public const ushort C2R_GetAccount = 20103;
		 public const ushort R2C_CreateAccount = 20104;
		 public const ushort C2R_CreateAccount = 20105;
		 public const ushort R2C_CreateOnlineUnit = 20106;
		 public const ushort C2R_CreateOnlineUnit = 20107;
		 public const ushort Actor_GetOnlineUnitRequest = 20108;
		 public const ushort Actor_GetOnlineUnitResponse = 20109;
		 public const ushort Actor_RemoveOnlineUnit = 20110;
		 public const ushort R2G_GetLoginKey = 20111;
		 public const ushort G2R_GetLoginKey = 20112;
		 public const ushort G2L_LoginLobby = 20113;
		 public const ushort L2G_LoginLobby = 20114;
		 public const ushort Actor_Offline = 20115;
		 public const ushort AccessUserRequest = 20116;
		 public const ushort AccessUserResponse = 20117;
		 public const ushort ChangeUserRequest = 20118;
		 public const ushort ChangeUserResponse = 20119;
		 public const ushort G2F_LoginFriend = 20120;
		 public const ushort F2G_LoginFriend = 20121;
		 public const ushort G2M_CreateMatchUnit = 20122;
		 public const ushort M2G_CreateMatchUnit = 20123;
		 public const ushort Actor_RemoveMatchUnitRequest = 20124;
		 public const ushort Actor_RemoveMatchUnitResponse = 20125;
		 public const ushort M2R_GetRoom = 20126;
		 public const ushort R2M_GetRoom = 20127;
		 public const ushort Actor_RoomStart = 20128;
		 public const ushort Actor_RoomEnd = 20129;
		 public const ushort Actor_JoinRoomRequest = 20130;
		 public const ushort Actor_JoinRoomResponse = 20131;
		 public const ushort Actor_ExitRoomRequest = 20132;
		 public const ushort Actor_ExitRoomResponse = 20133;
		 public const ushort Actor_JoinRoomMessage = 20134;
		 public const ushort Actor_CreateUnit = 20135;
		 public const ushort Actor_RemoveUnit = 20136;
		 public const ushort Actor_ConnectRobotRequest = 20137;
		 public const ushort Actor_ConnectRobotResponse = 20138;
	}
}
