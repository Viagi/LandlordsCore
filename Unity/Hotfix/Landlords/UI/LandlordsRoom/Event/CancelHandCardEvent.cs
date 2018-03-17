using ETModel;

namespace ETHotfix
{
    [Event(ETModel.EventIdType.CancelHandCard)]
    public class CancelHandCardEvent : AEvent<Card>
    {
        public override void Run(Card card)
        {
            Game.Scene.GetComponent<UIComponent>().Get(UIType.LandlordsRoom).GetComponent<LandlordsRoomComponent>().Interaction.CancelCard(card);
        }
    }
}
