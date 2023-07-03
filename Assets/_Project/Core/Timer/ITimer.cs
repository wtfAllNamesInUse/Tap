using System;

namespace TapTapTap.Core
{
    public interface ITimer
    {
        bool IsRunning { get; }
        string Id { get; }
        double ElapsedSeconds { get; }
        TimeSpan ElapsedTime { get; }

        void Start();
        void Stop();
    }
}