namespace Model
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class AccountInfo : Entity
    {
        //用户名
        public string Account { get; set; }

        //密码
        public string Password { get; set; }
    }
}
