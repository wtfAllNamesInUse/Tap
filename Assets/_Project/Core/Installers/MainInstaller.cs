using System.Collections.Generic;
using TapTapTap.Core.FSM;
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

        public override void InstallBindings()
        {
            InstallSignals();

            Container.Bind<GameStateData>().AsSingle();
            Container.BindInstance(cameraController).AsSingle();

            Container.Bind<SpawnerSystem>().AsSingle();
            Container.Bind<ArchetypeProvider>().AsSingle();

            Container.BindFactory<Object, EntityData, Entity, Entity.Factory>()
                .FromFactory<PrefabFactory<EntityData, Entity>>();

            Container.BindInstance(positionProvider).AsSingle();
            Container.BindInterfacesTo<SimpleWaveController>().AsSingle();

            Container.BindFactory<IOwner, EntityStateMachine, EntityStateMachine.Factory>();
            Container.BindFactory<IOwner, List<EntityState>, EntityStatesFactory>()
                .FromFactory<EntityStatesCustomFactory>();
            Container.BindFactory<Blackboard, Blackboard.Factory>();

            Container.BindFactory<IdleState, IdleState.Factory>();
            Container.BindFactory<RunState, RunState.Factory>();
            Container.BindFactory<AttackState, AttackState.Factory>();

            Container.BindInterfacesTo<GameController>().AsSingle();

            Container.BindInterfacesAndSelfTo<ClickDetector>().AsSingle();
            Container.Bind<EntityGatherer>().AsSingle();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
        }
    }
}