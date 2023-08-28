using Zenject;

namespace TapTapTap.Core
{
    public class InputHandlerInstaller : Installer<InputHandlerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ITickable), typeof(ClickDetector)).To<ClickDetector>().AsSingle();
            Container.Bind(typeof(ITickable), typeof(SwipeDetector)).To<SwipeDetector>().AsSingle();
        }
    }
}