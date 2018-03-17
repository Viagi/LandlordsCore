using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Gate)]
    public class Actor_MatchSucess_NttHandler : AMActorHandler<User, Actor_MatchSucess_Ntt>
    {
        protected override async Task Run(User user, Actor_MatchSucess_Ntt message)
        {
            user.IsMatching = false;
            user.ActorID = message.GamerID;
            Log.Info($"玩家{user.UserID}匹配成功");

            await Task.CompletedTask;
        }
    }
}
