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
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Model
{
    #region Base

    [ProtoInclude(Opcode.C2G_ReturnLobby_Ntt, typeof(C2G_ReturnLobby_Ntt))]
    [ProtoInclude(Opcode.G2C_PlayerDisconnect_Ntt, typeof(G2C_PlayerDisconnect_Ntt))]
    public abstract partial class AMessage
    {
    }

    [ProtoInclude(Opcode.C2R_Login_Req, typeof(C2R_Login_Req))]
    [ProtoInclude(Opcode.C2R_Register_Req, typeof(C2R_Register_Req))]
    [ProtoInclude(Opcode.C2G_LoginGate_Req, typeof(C2G_LoginGate_Req))]
    [ProtoInclude(Opcode.C2G_GetUserInfo_Req, typeof(C2G_GetUserInfo_Req))]
    [ProtoInclude(Opcode.C2G_StartMatch_Req, typeof(C2G_StartMatch_Req))]
    public abstract partial class ARequest : AMessage
    {
    }

    [ProtoInclude(Opcode.R2C_Login_Ack, typeof(R2C_Login_Ack))]
    [ProtoInclude(Opcode.R2C_Register_Ack, typeof(R2C_Register_Ack))]
    [ProtoInclude(Opcode.G2C_LoginGate_Ack, typeof(G2C_LoginGate_Ack))]
    [ProtoInclude(Opcode.G2C_GetUserInfo_Ack, typeof(G2C_GetUserInfo_Ack))]
    [ProtoInclude(Opcode.G2C_StartMatch_Ack, typeof(G2C_StartMatch_Ack))]
    public abstract partial class AResponse : AMessage
    {
    }

    [ProtoInclude(Opcode.Actor_GamerReady_Ntt, typeof(Actor_GamerReady_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerGrabLandlordSelect_Ntt, typeof(Actor_GamerGrabLandlordSelect_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerPlayCard_Ntt, typeof(Actor_GamerPlayCard_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerDontPlay_Ntt, typeof(Actor_GamerDontPlay_Ntt))]
    [ProtoInclude(Opcode.Actor_Trusteeship_Ntt, typeof(Actor_Trusteeship_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerEnterRoom_Ntt, typeof(Actor_GamerEnterRoom_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerExitRoom_Ntt, typeof(Actor_GamerExitRoom_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerReconnect_Ntt, typeof(Actor_GamerReconnect_Ntt))]
    [ProtoInclude(Opcode.Actor_GameStart_Ntt, typeof(Actor_GameStart_Ntt))]
    [ProtoInclude(Opcode.Actor_AuthorityGrabLandlord_Ntt, typeof(Actor_AuthorityGrabLandlord_Ntt))]
    [ProtoInclude(Opcode.Actor_AuthorityPlayCard_Ntt, typeof(Actor_AuthorityPlayCard_Ntt))]
    [ProtoInclude(Opcode.Actor_SetMultiples_Ntt, typeof(Actor_SetMultiples_Ntt))]
    [ProtoInclude(Opcode.Actor_SetLandlord_Ntt, typeof(Actor_SetLandlord_Ntt))]
    [ProtoInclude(Opcode.Actor_Gameover_Ntt, typeof(Actor_Gameover_Ntt))]
    [ProtoInclude(Opcode.Actor_GamerMoneyLess_Ntt,typeof(Actor_GamerMoneyLess_Ntt))]
    [BsonKnownTypes(typeof(Actor_GamerReady_Ntt))]
    [BsonKnownTypes(typeof(Actor_GamerGrabLandlordSelect_Ntt))]
    [BsonKnownTypes(typeof(Actor_GamerPlayCard_Ntt))]
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
    public abstract partial class AActorMessage : AMessage
    {
    }

    [ProtoInclude(Opcode.Actor_GamerPlayCard_Req, typeof(Actor_GamerPlayCard_Req))]
    [ProtoInclude(Opcode.Actor_GamerPrompt_Req, typeof(Actor_GamerPrompt_Req))]
    [BsonKnownTypes(typeof(Actor_GamerPlayCard_Req))]
    [BsonKnownTypes(typeof(Actor_GamerPrompt_Req))]
    public abstract partial class AActorRequest : ARequest
    {
    }

    [ProtoInclude(Opcode.Actor_GamerPlayCard_Ack, typeof(Actor_GamerPlayCard_Ack))]
    [ProtoInclude(Opcode.Actor_GamerPrompt_Ack, typeof(Actor_GamerPrompt_Ack))]
    [BsonKnownTypes(typeof(Actor_GamerPlayCard_Ack))]
    [BsonKnownTypes(typeof(Actor_GamerPrompt_Ack))]
    public abstract partial class AActorResponse : AResponse
    {
    }

    #endregion

    #region Client-Realm

    [ProtoContract]
    [Message(Opcode.C2R_Login_Req)]
    public class C2R_Login_Req : ARequest
    {
        [ProtoMember(1)]
        public string Account;
        [ProtoMember(2)]
        public string Password;
    }

    [ProtoContract]
    [Message(Opcode.R2C_Login_Ack)]
    public class R2C_Login_Ack : AResponse
    {
        [ProtoMember(1)]
        public long Key;
        [ProtoMember(2)]
        public string Address;
    }

    [ProtoContract]
    [Message(Opcode.C2R_Register_Req)]
    public class C2R_Register_Req : ARequest
    {
        [ProtoMember(1)]
        public string Account;
        [ProtoMember(2)]
        public string Password;
    }

    [ProtoContract]
    [Message(Opcode.R2C_Register_Ack)]
    public class R2C_Register_Ack : AResponse
    {

    }

    #endregion

    #region Client-Gate

    [ProtoContract]
    [Message(Opcode.C2G_LoginGate_Req)]
    public class C2G_LoginGate_Req : ARequest
    {
        [ProtoMember(1)]
        public long Key;
    }

    [ProtoContract]
    [Message(Opcode.G2C_LoginGate_Ack)]
    public class G2C_LoginGate_Ack : AResponse
    {
        [ProtoMember(1)]
        public long PlayerID;
        [ProtoMember(2)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.C2G_GetUserInfo_Req)]
    public class C2G_GetUserInfo_Req : ARequest
    {
        [ProtoMember(1)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.G2C_GetUserInfo_Ack)]
    public class G2C_GetUserInfo_Ack : AResponse
    {
        [ProtoMember(1)]
        public string NickName;
        [ProtoMember(2)]
        public int Wins;
        [ProtoMember(3)]
        public int Loses;
        [ProtoMember(4)]
        public long Money;
    }

    [ProtoContract]
    [Message(Opcode.C2G_StartMatch_Req)]
    public class C2G_StartMatch_Req : ARequest
    {

    }

    [ProtoContract]
    [Message(Opcode.G2C_StartMatch_Ack)]
    public class G2C_StartMatch_Ack : AResponse
    {

    }

    [ProtoContract]
    [Message(Opcode.C2G_ReturnLobby_Ntt)]
    public class C2G_ReturnLobby_Ntt : AMessage
    {

    }

    #endregion

    #region Client-Map

    [ProtoContract]
    [Message(Opcode.Actor_GamerReady_Ntt)]
    public class Actor_GamerReady_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerGrabLandlordSelect_Ntt)]
    public class Actor_GamerGrabLandlordSelect_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public bool IsGrab;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerPlayCard_Req)]
    public class Actor_GamerPlayCard_Req : AActorRequest
    {
        [ProtoMember(1)]
        public Card[] Cards;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerPlayCard_Ack)]
    public class Actor_GamerPlayCard_Ack : AActorResponse
    {

    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerPlayCard_Ntt)]
    public class Actor_GamerPlayCard_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public Card[] Cards;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerPrompt_Req)]
    public class Actor_GamerPrompt_Req : AActorRequest
    {

    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerPrompt_Ack)]
    public class Actor_GamerPrompt_Ack : AActorResponse
    {
        [ProtoMember(1)]
        public Card[] Cards;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerDontPlay_Ntt)]
    public class Actor_GamerDontPlay_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.Actor_Trusteeship_Ntt)]
    public class Actor_Trusteeship_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public bool isTrusteeship;
    }

    #endregion

    #region Gate-Client

    [ProtoContract]
    [Message(Opcode.G2C_PlayerDisconnect_Ntt)]
    public class G2C_PlayerDisconnect_Ntt : AMessage
    {

    }

    #endregion

    #region Map-Client

    [ProtoContract]
    public class GamerInfo
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public bool IsReady;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerEnterRoom_Ntt)]
    public class Actor_GamerEnterRoom_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public List<GamerInfo> Gamers = new List<GamerInfo>();
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerExitRoom_Ntt)]
    public class Actor_GamerExitRoom_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerReconnect_Ntt)]
    public class Actor_GamerReconnect_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public int Multiples;
        [ProtoMember(2)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, Identity> GamersIdentity;
        [ProtoMember(3)]
        public Card[] LordCards;
        [ProtoMember(4)]
        public KeyValuePair<long, Card[]> DeskCards;
        [ProtoMember(5)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, bool> GamerGrabLandlordState;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GameStart_Ntt)]
    public class Actor_GameStart_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public Card[] GamerCards;
        [ProtoMember(2)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, int> GamerCardsNum;
    }

    [ProtoContract]
    [Message(Opcode.Actor_AuthorityGrabLandlord_Ntt)]
    public class Actor_AuthorityGrabLandlord_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
    }

    [ProtoContract]
    [Message(Opcode.Actor_AuthorityPlayCard_Ntt)]
    public class Actor_AuthorityPlayCard_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public bool IsFirst;
    }

    [ProtoContract]
    [Message(Opcode.Actor_SetMultiples_Ntt)]
    public class Actor_SetMultiples_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public int Multiples;
    }

    [ProtoContract]
    [Message(Opcode.Actor_SetLandlord_Ntt)]
    public class Actor_SetLandlord_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
        [ProtoMember(2)]
        public Card[] LordCards;
    }

    [ProtoContract]
    [Message(Opcode.Actor_Gameover_Ntt)]
    public class Actor_Gameover_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public Identity Winner;
        [ProtoMember(2)]
        public long BasePointPerMatch;
        [ProtoMember(3)]
        public int Multiples;
        [ProtoMember(4)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, long> GamersScore;
    }

    [ProtoContract]
    [Message(Opcode.Actor_GamerMoneyLess_Ntt)]
    public class Actor_GamerMoneyLess_Ntt : AActorMessage
    {
        [ProtoMember(1)]
        public long UserID;
    }

    #endregion
}
