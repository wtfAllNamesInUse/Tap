namespace TapTapTap.Core
{
    public interface IInputResolver
    {
        void RegisterClick();
        void RegisterSwipe(SwipeDirection swipeDirection);
        InputEventBase TryResolve();
    }
}