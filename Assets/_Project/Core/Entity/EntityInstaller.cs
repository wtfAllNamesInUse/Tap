using System.Collections.Generic;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class EntityInstaller : Installer<EntityInstaller>
    {
        [InjectOptional]
        private EntityData entityData;

        public override void InstallBindings()
        {
            Container.BindInstance(entityData);

            Container.BindFactory<EntityStateMachine, EntityStateMachine.Factory>();
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
        }
    }
}