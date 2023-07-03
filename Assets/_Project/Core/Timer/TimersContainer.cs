using System;
using System.Collections.Generic;
using Zenject;

namespace TapTapTap.Core
{
    public class TimersContainer : IInitializable, IDisposable, ITimersContainer
    {
        private readonly Timer.Factory timerFactory;
        private readonly IDictionary<string, ITimer> timers = new Dictionary<string, ITimer>();

        protected TimersContainer(Timer.Factory timerFactory)
        {
            this.timerFactory = timerFactory;
        }

        public void Initialize()
        {
            foreach (var timer in timers) {
                timer.Value.Start();
            }
        }

        public virtual void Dispose()
        {
            foreach (var timer in timers) {
                timer.Value.Stop();
            }
        }

        public ITimer AddTimer(string id)
        {
            var timer = timerFactory.Create(id);
            timers[timer.Id] = timer;

            return timer;
        }

        public ITimer GetTimer(string id)
        {
            if (timers.TryGetValue(id, out ITimer timer)) {
                return timer;
            }

            return null;
        }
    }
}