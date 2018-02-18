namespace Model
{
    public static partial class Opcode
    {
        #region Gate-Realm

        public const ushort G2R_PlayerOnline_Ntt = 3100;

        public const ushort G2R_PlayerOffline_Ntt = 3101;

        #endregion

        #region Realm-Gate

        public const ushort R2G_PlayerKickOut_Req = 3200;
        public const ushort G2R_PlayerKickOut_Ack = 3201;

        public const ushort R2G_GetLoginKey_Req = 3202;
        public const ushort G2R_GetLoginKey_Ack = 3203;

        #endregion

        #region Gate-Match

        public const ushort M2G_PlayerEnterMatch_Ack = 3300;
        public const ushort G2M_PlayerEnterMatch_Req = 3301;

        public const ushort G2M_PlayerExitMatch_Req = 3302;
        public const ushort M2G_PlayerExitMatch_Ack = 3303;

        #endregion

        #region Gate-Map

        public const ushort Actor_PlayerExitRoom_Req = 3400;
        public const ushort Actor_PlayerExitRoom_Ack = 3401;

        #endregion

        #region Match-Gate

        public const ushort Actor_MatchSucess_Ntt = 3500;

        #endregion

        #region Match-Map

        public const ushort MH2MP_CreateRoom_Req = 3600;
        public const ushort MP2MH_CreateRoom_Ack = 3601;

        public const ushort Actor_PlayerEnterRoom_Req = 3602;

        public const ushort Actor_PlayerEnterRoom_Ack = 3603;
        #endregion

        #region Map-Match

        public const ushort MP2MH_PlayerExitRoom_Ntt = 3700;

        public const ushort MP2MH_SyncRoomState_Ntt = 3701;

        #endregion
    }
}