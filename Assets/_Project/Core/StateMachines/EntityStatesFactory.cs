using System.Collections.Generic;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class EntityStatesFactory : PlaceholderFactory<IOwner, List<EntityState>>
    {
    }

    public class EntityStatesCustomFactory : IFactory<IOwner, List<EntityState>>
    {
        private readonly IdleState.Factory idleStateFactory;
        private readonly RunState.Factory runStateFactory;
        private readonly AttackState.Factory attackStateFactory;

        public EntityStatesCustomFactory(
            IdleState.Factory idleStateFactory,
            RunState.Factory runStateFactory,
            AttackState.Factory attackStateFactory)
        {
            this.idleStateFactory = idleStateFactory;
            this.runStateFactory = runStateFactory;
            this.attackStateFactory = attackStateFactory;
        }

        public List<EntityState> Create(IOwner owner)
        {
            var result = new List<EntityState> {
                idleStateFactory.Create(),
                runStateFactory.Create(),
                attackStateFactory.Create(),
            };

            return result;
        }
    }
}