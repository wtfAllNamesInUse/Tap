using System;
using TapTapTap.DateTimeProvider;
using Zenject;

namespace TapTapTap.Core
{
    public class Timer : ITimer
    {
        public double ElapsedSeconds => ElapsedTime.TotalSeconds;
        public TimeSpan ElapsedTime => DateTime.Now - dateTimeStart;
        public bool IsRunning => isRunning;
        public string Id => id;

        private DateTime dateTimeStart;
        private bool isRunning;
        
        private readonly string id;
        private readonly IDateTimeProvider dateTimeProvider;

        public Timer(
            string id,
            IDateTimeProvider dateTimeProvider)
        {
            this.id = id;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Start()
        {
            dateTimeStart = dateTimeProvider.CurrentDateTime;
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public class Factory : PlaceholderFactory<string, Timer>
        {
        }
    }
}