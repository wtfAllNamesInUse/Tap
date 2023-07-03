using System;
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

        public Timer(string id)
        {
            this.id = id;
        }

        public void Start()
        {
            dateTimeStart = DateTime.Now;
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