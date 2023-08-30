using System.Collections.Generic;
using System.Linq;
using TapTapTap.DateTimeProvider;
using Zenject;

namespace TapTapTap.ConfigurableTickables
{
    public class ConfigurableTickablesManager : ITickable
    {
        private const int SecondsToMiliSecondsMultiplier = 1000;

        private readonly SortedList<int, IList<IConfigurableTickable>> registeredTickables =
            new SortedList<int, IList<IConfigurableTickable>>();

        private readonly IDateTimeProvider dateTimeProvider;
        
        private float timer;
        private int nextTick;

        public ConfigurableTickablesManager(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Register(IConfigurableTickable configurableTickable)
        {
            var time = configurableTickable.TickEveryNthMilisecond;
            if (!registeredTickables.TryGetValue(time, out _)) {
                registeredTickables[time] = new List<IConfigurableTickable>();
            }

            registeredTickables[time].Add(configurableTickable);

            SelectNextTick();
        }

        public void Unregister(IConfigurableTickable configurableTickable)
        {
            var time = configurableTickable.TickEveryNthMilisecond;
            if (registeredTickables.TryGetValue(time, out var result)) {
                result.Remove(configurableTickable);
            }
        }

        public void Tick()
        {
            if (nextTick <= 0) {
                return;
            }

            timer += dateTimeProvider.DeltaTime * SecondsToMiliSecondsMultiplier;
            if (timer < nextTick) {
                return;
            }

            foreach (var tickable in registeredTickables[nextTick]) {
                tickable.ConfigurableTick();
            }
            // timer -= nextTick;

            SelectNextTick();
        }

        private void SelectNextTick()
        {
            var keys = registeredTickables.Keys;
            nextTick = keys.FirstOrDefault(p => p > nextTick);

            if (nextTick == 0 && registeredTickables.Count > 0) {
                nextTick = keys[0];
            }
        }
    }
}