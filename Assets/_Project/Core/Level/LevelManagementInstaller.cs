using Zenject;

namespace TapTapTap.Core
{
    public class LevelManagementInstaller : Installer<LevelManagementInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(LevelsManager)).To<LevelsManager>().AsSingle();
            Container.Bind<LevelsContainer>().AsSingle();
            Container.Bind(typeof(ILevelProvider)).To<LevelProviderFromBuiltInLevels>().AsSingle();
            Container.Bind(typeof(ILevelConverter)).To<LevelConverter>().AsSingle();
        }
    }
}