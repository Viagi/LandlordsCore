using ProtoBuf;
using Model;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Model
{
	#region ET
/// <summary>
/// 传送unit
/// </summary>
	[Message(InnerOpcode.M2M_TrasferUnitRequest)]
	[ProtoContract]
	public partial class M2M_TrasferUnitRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public Unit Unit;

	}

	[Message(InnerOpcode.M2M_TrasferUnitResponse)]
	[ProtoContract]
	public partial class M2M_TrasferUnitResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.M2A_Reload)]
	[ProtoContract]
	public partial class M2A_Reload: IRequest
	{
	}

	[Message(InnerOpcode.A2M_Reload)]
	[ProtoContract]
	public partial class A2M_Reload: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2G_LockRequest)]
	[ProtoContract]
	public partial class G2G_LockRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Id;

		[ProtoMember(2, IsRequired = true)]
		public string Address;

	}

	[Message(InnerOpcode.G2G_LockResponse)]
	[ProtoContract]
	public partial class G2G_LockResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2G_LockReleaseRequest)]
	[ProtoContract]
	public partial class G2G_LockReleaseRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Id;

		[ProtoMember(2, IsRequired = true)]
		public string Address;

	}

	[Message(InnerOpcode.G2G_LockReleaseResponse)]
	[ProtoContract]
	public partial class G2G_LockReleaseResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBSaveRequest)]
	[ProtoContract]
	public partial class DBSaveRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public bool NeedCache;

		[ProtoMember(2, IsRequired = true)]
		public string CollectionName;

		[ProtoMember(3, IsRequired = true)]
		public Component Disposer;

	}

	[Message(InnerOpcode.DBSaveBatchResponse)]
	[ProtoContract]
	public partial class DBSaveBatchResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBSaveBatchRequest)]
	[ProtoContract]
	public partial class DBSaveBatchRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public bool NeedCache;

		[ProtoMember(2, IsRequired = true)]
		public string CollectionName;

		[ProtoMember(3)]
		public List<Component> Disposers = new List<Component>();

	}

	[Message(InnerOpcode.DBSaveResponse)]
	[ProtoContract]
	public partial class DBSaveResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.DBQueryRequest)]
	[ProtoContract]
	public partial class DBQueryRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Id;

		[ProtoMember(2, IsRequired = true)]
		public string CollectionName;

		[ProtoMember(3, IsRequired = true)]
		public bool NeedCache;

	}

	[Message(InnerOpcode.DBQueryResponse)]
	[ProtoContract]
	public partial class DBQueryResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public Component Disposer;

	}

	[Message(InnerOpcode.DBQueryBatchRequest)]
	[ProtoContract]
	public partial class DBQueryBatchRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public string CollectionName;

		[ProtoMember(2)]
		public List<long> IdList = new List<long>();

		[ProtoMember(3, IsRequired = true)]
		public bool NeedCache;

	}

	[Message(InnerOpcode.DBQueryBatchResponse)]
	[ProtoContract]
	public partial class DBQueryBatchResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<Component> Disposers = new List<Component>();

	}

	[Message(InnerOpcode.DBQueryJsonRequest)]
	[ProtoContract]
	public partial class DBQueryJsonRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public string CollectionName;

		[ProtoMember(2, IsRequired = true)]
		public string Json;

		[ProtoMember(3, IsRequired = true)]
		public bool NeedCache;

	}

	[Message(InnerOpcode.DBQueryJsonResponse)]
	[ProtoContract]
	public partial class DBQueryJsonResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1)]
		public List<Component> Disposers = new List<Component>();

	}

	[Message(InnerOpcode.ObjectAddRequest)]
	[ProtoContract]
	public partial class ObjectAddRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

		[ProtoMember(2, IsRequired = true)]
		public int AppId;

	}

	[Message(InnerOpcode.ObjectAddResponse)]
	[ProtoContract]
	public partial class ObjectAddResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectRemoveRequest)]
	[ProtoContract]
	public partial class ObjectRemoveRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(InnerOpcode.ObjectRemoveResponse)]
	[ProtoContract]
	public partial class ObjectRemoveResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectLockRequest)]
	[ProtoContract]
	public partial class ObjectLockRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

		[ProtoMember(2, IsRequired = true)]
		public int LockAppId;

		[ProtoMember(3, IsRequired = true)]
		public int Time;

	}

	[Message(InnerOpcode.ObjectLockResponse)]
	[ProtoContract]
	public partial class ObjectLockResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectUnLockRequest)]
	[ProtoContract]
	public partial class ObjectUnLockRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

		[ProtoMember(2, IsRequired = true)]
		public int UnLockAppId;

		[ProtoMember(3, IsRequired = true)]
		public int AppId;

	}

	[Message(InnerOpcode.ObjectUnLockResponse)]
	[ProtoContract]
	public partial class ObjectUnLockResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.ObjectGetRequest)]
	[ProtoContract]
	public partial class ObjectGetRequest: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(InnerOpcode.ObjectGetResponse)]
	[ProtoContract]
	public partial class ObjectGetResponse: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public int AppId;

	}

	[Message(InnerOpcode.R2G_GetLoginKey)]
	[ProtoContract]
	public partial class R2G_GetLoginKey: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public string Account;

	}

	[Message(InnerOpcode.G2R_GetLoginKey)]
	[ProtoContract]
	public partial class G2R_GetLoginKey: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(InnerOpcode.G2M_CreateUnit)]
	[ProtoContract]
	public partial class G2M_CreateUnit: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long PlayerId;

		[ProtoMember(2, IsRequired = true)]
		public long GateSessionId;

	}

	[Message(InnerOpcode.M2G_CreateUnit)]
	[ProtoContract]
	public partial class M2G_CreateUnit: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long UnitId;

		[ProtoMember(2, IsRequired = true)]
		public int Count;

	}

	#endregion
	#region Gate-Realm
	[Message(InnerOpcode.G2R_PlayerOnline_Ntt)]
	[ProtoContract]
	public partial class G2R_PlayerOnline_Ntt: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

		[ProtoMember(2, IsRequired = true)]
		public int GateAppID;

	}

	[Message(InnerOpcode.G2R_PlayerOffline_Ntt)]
	[ProtoContract]
	public partial class G2R_PlayerOffline_Ntt: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	#endregion
	#region Realm-Gate
	[Message(InnerOpcode.R2G_GetLoginKey_Req)]
	[ProtoContract]
	public partial class R2G_GetLoginKey_Req: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(InnerOpcode.G2R_GetLoginKey_Ack)]
	[ProtoContract]
	public partial class G2R_GetLoginKey_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long Key;

	}

	[Message(InnerOpcode.R2G_PlayerKickOut_Req)]
	[ProtoContract]
	public partial class R2G_PlayerKickOut_Req: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(InnerOpcode.G2R_PlayerKickOut_Ack)]
	[ProtoContract]
	public partial class G2R_PlayerKickOut_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	#endregion
	#region Gate-Match
	[Message(InnerOpcode.G2M_PlayerEnterMatch_Req)]
	[ProtoContract]
	public partial class G2M_PlayerEnterMatch_Req: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long PlayerID;

		[ProtoMember(2, IsRequired = true)]
		public long UserID;

		[ProtoMember(3, IsRequired = true)]
		public long SessionID;

	}

	[Message(InnerOpcode.M2G_PlayerEnterMatch_Ack)]
	[ProtoContract]
	public partial class M2G_PlayerEnterMatch_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	[Message(InnerOpcode.G2M_PlayerExitMatch_Req)]
	[ProtoContract]
	public partial class G2M_PlayerExitMatch_Req: IRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(InnerOpcode.M2G_PlayerExitMatch_Ack)]
	[ProtoContract]
	public partial class M2G_PlayerExitMatch_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	#endregion
	#region Gate-Map
	[Message(InnerOpcode.Actor_PlayerExitRoom_Req)]
	[ProtoContract]
	public partial class Actor_PlayerExitRoom_Req: MessageObject, IActorRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long UserID;

	}

	[Message(InnerOpcode.Actor_PlayerExitRoom_Ack)]
	[ProtoContract]
	public partial class Actor_PlayerExitRoom_Ack: MessageObject, IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

	}

	#endregion
	#region Match-Gate
	[Message(InnerOpcode.Actor_MatchSucess_Ntt)]
	[ProtoContract]
	public partial class Actor_MatchSucess_Ntt: MessageObject, IActorMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long GamerID;

	}

	#endregion
	#region Match-Map
	[Message(InnerOpcode.MH2MP_CreateRoom_Req)]
	[ProtoContract]
	public partial class MH2MP_CreateRoom_Req: IRequest
	{
	}

	[Message(InnerOpcode.MP2MH_CreateRoom_Ack)]
	[ProtoContract]
	public partial class MP2MH_CreateRoom_Ack: IResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long RoomID;

	}

	[Message(InnerOpcode.Actor_PlayerEnterRoom_Req)]
	[ProtoContract]
	public partial class Actor_PlayerEnterRoom_Req: MessageObject, IActorRequest
	{
		[ProtoMember(1, IsRequired = true)]
		public long PlayerID;

		[ProtoMember(2, IsRequired = true)]
		public long UserID;

		[ProtoMember(3, IsRequired = true)]
		public long SessionID;

	}

	[Message(InnerOpcode.Actor_PlayerEnterRoom_Ack)]
	[ProtoContract]
	public partial class Actor_PlayerEnterRoom_Ack: MessageObject, IActorResponse
	{
		[ProtoMember(90, IsRequired = true)]
		public int Error { get; set; }

		[ProtoMember(91, IsRequired = true)]
		public string Message { get; set; }

		[ProtoMember(1, IsRequired = true)]
		public long GamerID;

	}

	#endregion
	#region Map-Match
	[Message(InnerOpcode.MP2MH_PlayerExitRoom_Ntt)]
	[ProtoContract]
	public partial class MP2MH_PlayerExitRoom_Ntt: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long RoomID;

		[ProtoMember(2, IsRequired = true)]
		public long UserID;

	}

	[Message(InnerOpcode.MP2MH_SyncRoomState_Ntt)]
	[ProtoContract]
	public partial class MP2MH_SyncRoomState_Ntt: IMessage
	{
		[ProtoMember(1, IsRequired = true)]
		public long RoomID;

		[ProtoMember(2, IsRequired = true)]
		public RoomState State;

	}

	#endregion
}
#if SERVER
namespace Model
{
	[BsonKnownTypes(typeof(Actor_PlayerExitRoom_Req))]
	[BsonKnownTypes(typeof(Actor_PlayerExitRoom_Ack))]
	[BsonKnownTypes(typeof(Actor_MatchSucess_Ntt))]
	[BsonKnownTypes(typeof(Actor_PlayerEnterRoom_Req))]
	[BsonKnownTypes(typeof(Actor_PlayerEnterRoom_Ack))]
	public partial class MessageObject {}

}
#endif
namespace Model
{
	public partial class MessageObject {}

}
