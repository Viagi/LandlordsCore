namespace Model
{
    public static partial class Opcode
    {
        #region Client-Realm

        public const ushort C2R_Login_Req = 2000;
        public const ushort R2C_Login_Ack = 2001;

        public const ushort C2R_Register_Req = 2002;
        public const ushort R2C_Register_Ack = 2003;

        #endregion

        #region Client-Gate

        public const ushort C2G_StartMatch_Req = 2100;
        public const ushort G2C_StartMatch_Ack = 2101;

        public const ushort C2G_ReturnLobby_Ntt = 2102;

        public const ushort C2G_GetUserInfo_Req = 2103;
        public const ushort G2C_GetUserInfo_Ack = 2104;

        public const ushort C2G_LoginGate_Req = 2105;
        public const ushort G2C_LoginGate_Ack = 2106;

        #endregion

        #region Client-Map

        public const ushort Actor_GamerReady_Ntt = 2200;

        public const ushort Actor_GamerGrabLandlordSelect_Ntt = 2201;

        public const ushort Actor_GamerPlayCard_Req = 2202;
        public const ushort Actor_GamerPlayCard_Ack = 2203;
        public const ushort Actor_GamerPlayCard_Ntt = 2204;

        public const ushort Actor_GamerDontPlay_Ntt = 2205;

        public const ushort Actor_GamerPrompt_Req = 2206;
        public const ushort Actor_GamerPrompt_Ack = 2207;

        public const ushort Actor_Trusteeship_Ntt = 2208;

        #endregion

        #region Gate-Client

        public const ushort G2C_PlayerDisconnect_Ntt = 3000;

        #endregion

        #region Map-Client

        public const ushort Actor_GamerEnterRoom_Ntt = 3800;
        public const ushort Actor_GamerExitRoom_Ntt = 3801;

        public const ushort Actor_GamerReconnect_Ntt = 3802;

        public const ushort Actor_GameStart_Ntt = 3803;

        public const ushort Actor_AuthorityGrabLandlord_Ntt = 3804;

        public const ushort Actor_AuthorityPlayCard_Ntt = 3805;

        public const ushort Actor_SetMultiples_Ntt = 3806;

        public const ushort Actor_SetLandlord_Ntt = 3807;

        public const ushort Actor_Gameover_Ntt = 3808;

        public const ushort Actor_GamerMoneyLess_Ntt = 3809;

        #endregion
    }
}