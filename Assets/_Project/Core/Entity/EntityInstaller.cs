using System;
using System.Collections.Generic;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    public class EntityInstaller : Installer<EntityInstaller>
    {
        [InjectOptional]
        private EntityData entityData;

        public override void InstallBindings()
        {
            // TODO: it seems it is not possible to use customFactory with gameObject context, verify that!
            // TODO: parameters from factory are passed to installer in gameObjectContext which forces us to bind things during Inject phase
            // TODO: it kinda smells,
            
            Container.BindInstance(entityData);

            Container.BindFactory<EntityStateMachine, EntityStateMachine.Factory>()
                .FromFactory<EntityStateMachineCustomFactory>();
            Container.BindFactory<List<State>, EntityStatesFactory>()
                .FromFactory<EntityStatesCustomFactory>();
            Container.BindFactory<Blackboard, Blackboard.Factory>();

            Container.BindFactory<IdleState, IdleState.Factory>();
            Container.BindFactory<RunState, RunState.Factory>();
            Container.BindFactory<AttackState, AttackState.Factory>();

            Container.BindFactory<Object, EntityView, EntityView.Factory>()
                .FromFactory<EntityViewFactory>();
            Container.BindFactory<HealthBar, HealthBar.Factory>()
                .FromFactory<HealthBarFactory>();

            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(EntityAttributeTracker))
                .To<EntityAttributeTracker>().AsSingle();
            
            Container.Bind<Transform>().FromComponentOnRoot();
        }
    }
}