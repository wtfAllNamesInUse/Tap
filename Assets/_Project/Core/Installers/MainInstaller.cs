using System.Collections.Generic;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace TapTapTap.Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private PositionProvider positionProvider;

        [SerializeField]
        private CameraController cameraController;

        [SerializeField]
        private Canvas rootCanvas;

        [SerializeField]
        private new Camera camera;

        [SerializeField]
        private GameObject entity;

        public override void InstallBindings()
        {
            InstallSignals();

            Container.Bind<GameStateData>().AsSingle();
            Container.BindInstance(cameraController).AsSingle();

            Container.Bind<SpawnerSystem>().AsSingle();
            Container.Bind<ArchetypeProvider>().AsSingle();

            Container.BindFactory<EntityData, Entity, Entity.Factory>().FromSubContainerResolve().ByNewPrefabInstaller<EntityInstaller>(entity);

            Container.BindInstance(positionProvider).AsSingle();
            Container.BindInterfacesTo<DistanceBasedWaveController>().AsSingle();

            Container.BindInterfacesTo<GameController>().AsSingle();

            Container.BindInterfacesAndSelfTo<ClickDetector>().AsSingle();
            Container.Bind<EntityGatherer>().AsSingle();

            Container.BindInstance(rootCanvas).AsSingle();
            Container.BindInstance(camera).AsSingle();
            Container.Bind<ScreenController>().AsSingle();
            Container.Bind<IUiPrefabProvider>().To<UiPrefabProviderFromScriptableObject>().AsSingle();

            InstallTutorials();

            Container.Bind<SavedData>().AsSingle();

            InstallBlockers();

            Container.Bind(typeof(IInitializable), typeof(System.IDisposable)).To<SpeedSystem>().AsSingle().NonLazy();

            InstallTimers();
            InstallGameplayMechanics();

            Container.BindInterfacesAndSelfTo<DistanceEvaluator>().AsSingle();
            Container.BindInterfacesTo<LevelFinishEvaluator>().AsSingle();

            Container.BindInterfacesTo<PerksApplier>().AsSingle();
        }

        private void InstallGameplayMechanics()
        {
            Container.BindInterfacesTo<HealthRemovalMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SpeedRemovalMechanic>().AsSingle().NonLazy();
        }

        private void InstallTimers()
        {
            Container.BindInterfacesAndSelfTo<GameplayTimersContainer>().AsSingle().NonLazy();
            Container.BindFactory<string, Timer, Timer.Factory>();
        }

        private void InstallBlockers()
        {
            Container.Bind<IBlocker>().To<PlayerInputBlocker>().AsSingle().WhenInjectedInto(
                typeof(GameController),
                typeof(FirstLaunchTutorial),
                typeof(GameplayTutorialsContainer),
                typeof(SpeedSystem));
        }

        private void InstallTutorials()
        {
            Container.Bind<ITutorialsContainer>().To<GameplayTutorialsContainer>().AsSingle();
            Container.Bind<ITutorial>().To<FirstLaunchTutorial>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
        }
    }
}