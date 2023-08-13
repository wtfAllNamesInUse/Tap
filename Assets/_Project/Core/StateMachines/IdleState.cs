using Zenject;

namespace TapTapTap.Core.FSM
{
    public class IdleState : State
    {
        public override int StateID => EntityStates.Idle;

        public class Factory : PlaceholderFactory<IdleState>
        {
        }
    }
}