using Zenject;

namespace TapTapTap.Core.FSM
{
    public class IdleState : EntityState
    {
        public override int StateID => EntityStates.Idle;

        public class Factory : PlaceholderFactory<IdleState>
        {
        }
    }
}