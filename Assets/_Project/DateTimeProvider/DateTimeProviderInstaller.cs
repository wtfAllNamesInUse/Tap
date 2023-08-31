using Zenject;

namespace TapTapTap.DateTimeProvider
{
    public class DateTimeProviderInstaller : Installer<DateTimeProviderInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IDateTimeProvider>().To<SystemDateTimeProvider>().AsSingle();

#if UNITY_EDITOR
            var isEditorProviderEnabled = DateTimeProviderModel.LoadModel().isEnabled;
            if (isEditorProviderEnabled) {
                Container.Decorate<IDateTimeProvider>().With<EditorDateTimeProvider>();
            }
#endif
        }
    }
}