using System.Collections.Generic;
using Zenject;

namespace TapTapTap.Core
{
    public class InputResolver : IInputResolver
    {
        private readonly Dictionary<EventType, InputEventBase> events = new Dictionary<EventType, InputEventBase>();

        public void RegisterClick()
        {
            events[EventType.Click] = new ClickInputEvent();
        }

        public void RegisterSwipe(SwipeDirection swipeDirection)
        {
            events[EventType.Swipe] = new SwipeInputEvent(swipeDirection);
        }

        public InputEventBase TryResolve()
        {
            InputEventBase result = null;

            if (events.ContainsKey(EventType.Swipe)) {
                result = events[EventType.Swipe];
            }
            else if (events.ContainsKey(EventType.Click)) {
                result = events[EventType.Click];
            }

            events.Clear();
            return result;
        }
    }
}