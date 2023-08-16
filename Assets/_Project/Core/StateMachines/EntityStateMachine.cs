using TapTapTap.Core.FSM;
using Zenject;

namespace TapTapTap.Core
{
    public static class EntityStates
    {
        public const int Idle = 0;
        public const int Run = 1;
        public const int Attack = 2;
    }

    public class EntityStateMachine : StateMachine
    {
        private EntityStatesFactory entityStatesFactory;
        private Blackboard.Factory blackboardFactory;

        public EntityStateMachine(
            EntityStatesFactory entityStatesFactory,
            Blackboard.Factory blackboardFactory)
        {
            this.entityStatesFactory = entityStatesFactory;
            this.blackboardFactory = blackboardFactory;
        }

        public void Initialize()
        {
            var states = entityStatesFactory.Create();
            var blackboard = blackboardFactory.Create();
            DoInit(states, blackboard);
        }

        public class Factory : PlaceholderFactory<EntityStateMachine>
        {
        }
    }
}