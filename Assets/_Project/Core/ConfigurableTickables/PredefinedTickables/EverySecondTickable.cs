using System;

namespace TapTapTap.ConfigurableTickables
{
    public class EverySecondTickable : IConfigurableTickable
    {
        public event Action Tick;

        public int TickEveryNthMilisecond => 1000;

        private readonly ConfigurableTickablesManager configurableTickablesManager;

        public EverySecondTickable(
            ConfigurableTickablesManager configurableTickablesManager)
        {
            this.configurableTickablesManager = configurableTickablesManager;
        }

        public void Initialize()
        {
            configurableTickablesManager.Register(this);
        }

        void IConfigurableTickable.ConfigurableTick()
        {
            Tick?.Invoke();
        }

        public void Dispose()
        {
            configurableTickablesManager.Unregister(this);
        }
    }
}
