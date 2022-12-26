using System.IO;
using System.Text;

namespace ET.Client
{
    namespace Landlords
    {
        [ComponentOf(typeof(Scene))]
        public class ClientConsoleComponent : Entity, IAwake, IUpdate, IDestroy
        {
            public TextReader Default { get; set; }
            public TextReader Commond { get; set; }
        }
    }
}
