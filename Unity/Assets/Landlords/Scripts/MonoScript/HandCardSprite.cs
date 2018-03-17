using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ETModel
{
    public class HandCardSprite : MonoBehaviour
    {
        public Card Poker { get; set; }
        private bool isSelect;

        void Start()
        {
            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
            eventTrigger.triggers = new List<EventTrigger.Entry>();
            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback = new EventTrigger.TriggerEvent();
            clickEntry.callback.AddListener(new UnityAction<BaseEventData>(OnClick));
            eventTrigger.triggers.Add(clickEntry);
        }

        public void OnClick(BaseEventData data)
        {
            float move = 50.0f;
            if (isSelect)
            {
                move = -move;
                Game.EventSystem.Run(EventIdType.CancelHandCard, Poker);
            }
            else
            {
                Game.EventSystem.Run(EventIdType.SelectHandCard, Poker);
            }
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += Vector2.up * move;
            isSelect = !isSelect;
        }
    }
}
