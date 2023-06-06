using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "MainSoInstaller", menuName = "Installers/MainSoInstaller")]
    public class MainSoInstaller : ScriptableObjectInstaller<MainSoInstaller>
    {
        [SerializeField]
        private EntityConfig entityConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(entityConfig).AsSingle();
        }
    }
}