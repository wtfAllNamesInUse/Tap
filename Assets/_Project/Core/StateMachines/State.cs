using Zenject;

namespace TapTapTap.Core.FSM
{
    public abstract class State
    {
        public abstract int StateID { get; }

        public StateMachine StateMachine { get; set; }
        public Blackboard Blackboard { get; set; }

        protected Entity Owner;

        [Inject]
        protected void Initialize(Entity owner)
        {
            Owner = owner;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void Tick()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}