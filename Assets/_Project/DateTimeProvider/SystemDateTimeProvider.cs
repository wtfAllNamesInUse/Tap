using System;
using UnityEngine;

namespace TapTapTap.DateTimeProvider
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime CurrentDateTime => DateTime.Now;
        public DateTime CurrentUtcDateTime => DateTime.UtcNow;
        public float DeltaTime => Time.deltaTime;
    }
}
