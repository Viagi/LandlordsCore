using Model;

namespace Hotfix
{
    [Event(Model.EventIdType.SelectHandCard)]
    public class SelectHandCardEvent : AEvent<Card>
    {
        public override void Run(Card card)
        {
            Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom).GetComponent<LandlordsRoomComponent>().Interaction.SelectCard(card);
        }
    }
}
