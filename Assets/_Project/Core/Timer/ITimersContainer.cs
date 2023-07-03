namespace TapTapTap.Core
{
    public interface ITimersContainer
    {
        ITimer AddTimer(string id);
        ITimer GetTimer(string id);
    }
}