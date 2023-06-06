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
        public EntityStateMachine(
            EntityStatesFactory entityStatesFactory,
            Blackboard.Factory blackboardFactory,
            IOwner owner)
        {
            var states = entityStatesFactory.Create(owner);
            var blackboard = blackboardFactory.Create();
            DoInit(owner, states, blackboard);
        }

        public class Factory : PlaceholderFactory<IOwner, EntityStateMachine>
        {
        }
    }
}