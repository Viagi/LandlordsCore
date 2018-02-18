using Model;

namespace Hotfix
{
    [Event((int)Model.EventIdType.SelectHandCard)]
    public class SelectHandCardEvent : IEvent<Card>
    {
        public void Run(Card card)
        {
            Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom).GetComponent<LandlordsRoomComponent>().Interaction.SelectCard(card);
        }
    }
}
