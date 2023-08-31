using Zenject;

namespace TapTapTap.Ui
{
    public class UiInstaller : Installer<UiInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScreenController>().AsSingle();
        }
    }
}