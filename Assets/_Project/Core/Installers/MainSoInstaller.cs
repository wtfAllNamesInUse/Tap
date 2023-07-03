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
        private UiPrefabs screenPrefabs;

        [SerializeField]
        private GameplaySettings gameplaySettings;

        public override void InstallBindings()
        {
            Container.BindInstance(entityConfig).AsSingle();
            Container.BindInstance(screenPrefabs).AsSingle();
            Container.BindInstance(gameplaySettings).AsSingle();
        }
    }
}