using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Gate)]
    public class Actor_MatchSucess_NttHandler : AMActorHandler<User, Actor_MatchSucess_Ntt>
    {
        protected override async Task Run(User user, Actor_MatchSucess_Ntt message)
        {
            user.IsMatching = false;
            user.ActorID = message.GamerID;

            await Task.CompletedTask;
        }
    }
}
