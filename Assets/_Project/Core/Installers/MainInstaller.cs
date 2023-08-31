using TapTapTap.Blockers;
using TapTapTap.ConfigurableTickables;
using TapTapTap.Core.FSM;
using TapTapTap.DateTimeProvider;
using TapTapTap.Ui;
using UnityEngine;
using Zenject;

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

        [SerializeField]
        private GameObject collectibleFacade;

        [SerializeField]
        private DamagePopup damagePopupPrefab;

        public override void InstallBindings()
        {
            InstallSignals();

            Container.Bind<GameStateData>().AsSingle();
            Container.BindInstance(cameraController).AsSingle();

            Container.Bind<ISpawner>().To<SpawnerContainer>().AsSingle()
                .WhenInjectedInto(typeof(GameInitializer), typeof(GameController));
            Container.Bind<ISpawner>()
                .To<Spawner<Entity, Entity.Factory, IArchetypeProvider<EntityArchetype>, EntityArchetype>>()
                .AsSingle();
            Container.Bind<ISpawner>()
                .To<Spawner<CollectibleFacade, CollectibleFacade.Factory, IArchetypeProvider<CollectibleArchetype>,
                    CollectibleArchetype>>()
                .AsSingle();

            Container.Bind<IArchetypeProvider<EntityArchetype>>().To<EntityArchetypeProvider>().AsSingle();
            Container.Bind<IArchetypeProvider<CollectibleArchetype>>().To<CollectibleArchetypeProvider>().AsSingle();

            Container.BindFactory<EntityArchetype, Entity, Entity.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<EntityInstaller>(entity);

            Container.BindFactory<CollectibleArchetype, CollectibleFacade, CollectibleFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<CollectibleInstaller>(collectibleFacade);

            Container.BindInstance(positionProvider).AsSingle();
            // Container.BindInterfacesTo<DistanceBasedWaveController>().AsSingle();

            Container.BindInterfacesTo<GameController>().AsSingle();

            InputHandlerInstaller.Install(Container);

            Container.Bind<EntityGatherer>().AsSingle();

            Container.BindInstance(rootCanvas).AsSingle();
            Container.BindInstance(camera).AsSingle();
            
            UiInstaller.Install(Container);

            Container.Bind<IUiPrefabProvider>().To<UiPrefabProviderFromProviders>().AsSingle()
                .WhenInjectedInto(typeof(ScreenController), typeof(HealthBarFactory));
            Container.Bind<IUiPrefabProvider>().To<UiPrefabProviderFromPool>().AsSingle();
            Container.Bind<IUiPrefabProvider>().To<UiPrefabProviderFromScriptableObject>().AsSingle();

            Container.BindFactory<DamagePopup, DamagePopup.Factory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(damagePopupPrefab)
                    .UnderTransform(rootCanvas.transform));

            InstallTutorials();

            Container.Bind<SavedData>().AsSingle();

            InstallBlockers();

            InstallTimers();

            Container.BindInterfacesAndSelfTo<DistanceEvaluator>().AsSingle();
            Container.BindInterfacesTo<LevelFinishEvaluator>().AsSingle();

            Container.BindInterfacesTo<PerksApplier>().AsSingle();
            Container.Bind<IPerkProvider>().To<RandomPerkProvider>().AsSingle();

            LevelManagementInstaller.Install(Container);

            Container.Bind(typeof(IInitializable)).To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IEncounterResolver>().To<EncounterResolver>().AsSingle();

            Container.Bind(typeof(IInputResolver)).To<InputResolver>().AsSingle();
            
            InventoryInstaller.Install(Container);
            ConfigurableTickablesInstaller.Install(Container);
            DateTimeProviderInstaller.Install(Container);
        }

        private void InstallTimers()
        {
            // TODO: lets remove all BindInterfacesAndSelfTo and similiars
            Container.BindInterfacesAndSelfTo<GameplayTimersContainer>().AsSingle().NonLazy();
            Container.BindFactory<string, Timer, Timer.Factory>();
        }

        private void InstallBlockers()
        {
            Container.Bind<IBlocker>().To<PlayerInputBlocker>().AsSingle();
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