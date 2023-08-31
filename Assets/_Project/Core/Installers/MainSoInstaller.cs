using TapTapTap.Ui;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "MainSoInstaller", menuName = "Installers/MainSoInstaller")]
    public class MainSoInstaller : ScriptableObjectInstaller<MainSoInstaller>
    {
        [SerializeField]
        private EntityConfig entityConfig;
        [SerializeField]
        private CollectibleConfig collectibleyConfig;
        [SerializeField]
        private UiPrefabs screenPrefabs;
        [SerializeField]
        private GameplaySettings gameplaySettings;
        [SerializeField]
        private PerksConfig perksConfig;
        [SerializeField]
        private BuiltInLevelsContainer builtInLevelsContainer;
        [SerializeField]
        private GameplayMechanicModelsContainer gameplayMechanicModelsContainer;

        public override void InstallBindings()
        {
            Container.BindInstance(entityConfig).AsSingle();
            Container.BindInstance(collectibleyConfig).AsSingle();
            Container.BindInstance(screenPrefabs).AsSingle();
            Container.BindInstance(gameplaySettings).AsSingle();
            Container.BindInstance(perksConfig).AsSingle();
            Container.BindInstance(builtInLevelsContainer).AsSingle();
            Container.BindInstance(gameplayMechanicModelsContainer).AsSingle();
        }
    }
}