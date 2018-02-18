/*
 * ↓消息声明定义↓
 * X2Y：消息从X端发送到Y端
 * Y2X：消息从Y端发送到X端
 * Actor：Actor消息
 * Req：请求消息
 * Ack：响应消息
 * Ntt：普通消息
 * */

using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Model
{
    #region Base

    [BsonKnownTypes(typeof(G2R_PlayerOnline_Ntt))]
    [BsonKnownTypes(typeof(G2R_PlayerOffline_Ntt))]
    [BsonKnownTypes(typeof(MP2MH_PlayerExitRoom_Ntt))]
    [BsonKnownTypes(typeof(MP2MH_SyncRoomState_Ntt))]
    public abstract partial class AMessage
    {
    }

    [BsonKnownTypes(typeof(R2G_GetLoginKey_Req))]
    [BsonKnownTypes(typeof(R2G_PlayerKickOut_Req))]
    [BsonKnownTypes(typeof(G2M_PlayerEnterMatch_Req))]
    [BsonKnownTypes(typeof(G2M_PlayerExitMatch_Req))]
    [BsonKnownTypes(typeof(MH2MP_CreateRoom_Req))]
    public abstract partial class ARequest : AMessage
    {
    }

    [BsonKnownTypes(typeof(G2R_GetLoginKey_Ack))]
    [BsonKnownTypes(typeof(G2R_PlayerKickOut_Ack))]
    [BsonKnownTypes(typeof(M2G_PlayerEnterMatch_Ack))]
    [BsonKnownTypes(typeof(M2G_PlayerExitMatch_Ack))]
    [BsonKnownTypes(typeof(MP2MH_CreateRoom_Ack))]
    public abstract partial class AResponse : AMessage
    {
    }

    [BsonKnownTypes(typeof(Actor_MatchSucess_Ntt))]
    public abstract partial class AActorMessage : AMessage
    {
    }

    [BsonKnownTypes(typeof(Actor_PlayerExitRoom_Req))]
    [BsonKnownTypes(typeof(Actor_PlayerEnterRoom_Req))]
    public abstract partial class AActorRequest : ARequest
    {
    }

    [BsonKnownTypes(typeof(Actor_PlayerExitRoom_Ack))]
    [BsonKnownTypes(typeof(Actor_PlayerEnterRoom_Ack))]
    public abstract partial class AActorResponse : AResponse
    {
    }

    #endregion

    #region Gate-Realm

    [Message(Opcode.G2R_PlayerOnline_Ntt)]
    [BsonIgnoreExtraElements]
    public class G2R_PlayerOnline_Ntt : AMessage
    {
        public long UserID;
        public int GateAppID;
    }

    [Message(Opcode.G2R_PlayerOffline_Ntt)]
    [BsonIgnoreExtraElements]
    public class G2R_PlayerOffline_Ntt : AMessage
    {
        public long UserID;
    }

    #endregion

    #region Realm-Gate

    [Message(Opcode.R2G_GetLoginKey_Req)]
    [BsonIgnoreExtraElements]
    public class R2G_GetLoginKey_Req : ARequest
    {
        public long UserID;
    }

    [Message(Opcode.G2R_GetLoginKey_Ack)]
    [BsonIgnoreExtraElements]
    public class G2R_GetLoginKey_Ack : AResponse
    {
        public long Key;
    }

    [Message(Opcode.R2G_PlayerKickOut_Req)]
    [BsonIgnoreExtraElements]
    public class R2G_PlayerKickOut_Req : ARequest
    {
        public long UserID;
    }

    [Message(Opcode.G2R_PlayerKickOut_Ack)]
    [BsonIgnoreExtraElements]
    public class G2R_PlayerKickOut_Ack : AResponse
    {

    }

    #endregion

    #region Gate-Match

    [Message(Opcode.G2M_PlayerEnterMatch_Req)]
    [BsonIgnoreExtraElements]
    public class G2M_PlayerEnterMatch_Req : ARequest
    {
        public long PlayerID;
        public long UserID;
        public long SessionID;
    }

    [Message(Opcode.M2G_PlayerEnterMatch_Ack)]
    [BsonIgnoreExtraElements]
    public class M2G_PlayerEnterMatch_Ack : AResponse
    {

    }

    [Message(Opcode.G2M_PlayerExitMatch_Req)]
    [BsonIgnoreExtraElements]
    public class G2M_PlayerExitMatch_Req : ARequest
    {
        public long UserID;
    }

    [Message(Opcode.M2G_PlayerExitMatch_Ack)]
    [BsonIgnoreExtraElements]
    public class M2G_PlayerExitMatch_Ack : AResponse
    {

    }

    #endregion

    #region Gate-Map

    [Message(Opcode.Actor_PlayerExitRoom_Req)]
    [BsonIgnoreExtraElements]
    public class Actor_PlayerExitRoom_Req : AActorRequest
    {
        public long UserID;
    }

    [Message(Opcode.Actor_PlayerExitRoom_Ack)]
    [BsonIgnoreExtraElements]
    public class Actor_PlayerExitRoom_Ack : AActorResponse
    {

    }

    #endregion

    #region Match-Gate
    [Message(Opcode.Actor_MatchSucess_Ntt)]
    [BsonIgnoreExtraElements]
    public class Actor_MatchSucess_Ntt : AActorMessage
    {
        public long GamerID;
    }

    #endregion

    #region Match-Map

    [Message(Opcode.MH2MP_CreateRoom_Req)]
    [BsonIgnoreExtraElements]
    public class MH2MP_CreateRoom_Req : ARequest
    {

    }

    [Message(Opcode.MP2MH_CreateRoom_Ack)]
    [BsonIgnoreExtraElements]
    public class MP2MH_CreateRoom_Ack : AResponse
    {
        public long RoomID;
    }

    [Message(Opcode.Actor_PlayerEnterRoom_Req)]
    [BsonIgnoreExtraElements]
    public class Actor_PlayerEnterRoom_Req : AActorRequest
    {
        public long PlayerID;
        public long UserID;
        public long SessionID;
    }

    [Message(Opcode.Actor_PlayerEnterRoom_Ack)]
    [BsonIgnoreExtraElements]
    public class Actor_PlayerEnterRoom_Ack : AActorResponse
    {
        public long GamerID;
    }

    #endregion

    #region Map-Match

    [Message(Opcode.MP2MH_PlayerExitRoom_Ntt)]
    [BsonIgnoreExtraElements]
    public class MP2MH_PlayerExitRoom_Ntt : AMessage
    {
        public long RoomID;
        public long UserID;
    }

    [Message(Opcode.MP2MH_SyncRoomState_Ntt)]
    [BsonIgnoreExtraElements]
    public class MP2MH_SyncRoomState_Ntt : AMessage
    {
        public long RoomID;
        public RoomState State;
    }

    #endregion
}
