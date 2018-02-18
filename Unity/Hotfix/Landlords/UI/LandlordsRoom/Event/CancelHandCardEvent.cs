using Model;

namespace Hotfix
{
    [Event((int)Model.EventIdType.CancelHandCard)]
    public class CancelHandCardEvent : IEvent<Card>
    {
        public void Run(Card card)
        {
            Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom).GetComponent<LandlordsRoomComponent>().Interaction.CancelCard(card);
        }
    }
}
