using System;
using Zenject;

namespace TapTapTap.ConfigurableTickables
{
    public interface IConfigurableTickable : IInitializable, IDisposable
    {
        int TickEveryNthMilisecond { get; }

        internal void ConfigurableTick();
    }
}
