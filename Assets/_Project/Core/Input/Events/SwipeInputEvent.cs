namespace TapTapTap.Core
{
    public class SwipeInputEvent : InputEventBase
    {
        public override EventType EventType => EventType.Swipe;
        public SwipeDirection SwipeDirection => swipeDirection;

        private readonly SwipeDirection swipeDirection;

        public SwipeInputEvent(SwipeDirection swipeDirection)
        {
            this.swipeDirection = swipeDirection;
        }
    }
}
