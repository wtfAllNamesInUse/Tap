using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleInstaller : Installer<CollectibleInstaller>
    {
        [InjectOptional]
        private CollectibleArchetype data;

        public override void InstallBindings()
        {
            // TODO: it seems it is not possible to use customFactory with gameObject context, verify that!
            // TODO: parameters from factory are passed to installer in gameObjectContext which forces us to bind things during Inject phase
            // TODO: it kinda smells,
            
            Container.BindInstance(data);
            Container.Bind<Transform>().FromComponentOnRoot();
            
            Container.BindFactory<Object, CollectibleView, CollectibleView.Factory>()
                .FromFactory<CollectibleViewFactory>();
        }
    }
}