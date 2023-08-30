using System;
using Zenject;

namespace TapTapTap.ConfigurableTickables
{
    public class ConfigurableTickablesInstaller : Installer<ConfigurableTickablesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ConfigurableTickablesManager), typeof(ITickable)).To<ConfigurableTickablesManager>().AsSingle();
            Container.Bind(typeof(EverySecondTickable), typeof(IInitializable), typeof(IDisposable)).To<EverySecondTickable>().AsSingle();
        }
    }
}