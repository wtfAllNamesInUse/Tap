using System.Collections.Generic;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class EntityStatesFactory : PlaceholderFactory<List<State>>
    {
    }

    public class EntityStatesCustomFactory : IFactory<List<State>>
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

        public List<State> Create()
        {
            var result = new List<State> {
                idleStateFactory.Create(),
                runStateFactory.Create(),
                attackStateFactory.Create(),
            };

            return result;
        }
    }
}