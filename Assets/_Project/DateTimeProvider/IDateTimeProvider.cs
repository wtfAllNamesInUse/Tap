using System;

namespace TapTapTap.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime CurrentDateTime { get; }
        DateTime CurrentUtcDateTime { get; }
        
        float DeltaTime { get; }
    }
}