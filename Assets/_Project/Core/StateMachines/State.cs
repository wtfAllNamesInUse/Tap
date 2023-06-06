namespace TapTapTap.Core.FSM
{
    public abstract class State
    {
        public abstract int StateID { get; }

        protected internal IOwner Owner { get; set; }

        public StateMachine StateMachine { get; set; }
        public Blackboard Blackboard { get; set; }

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

    public interface IOwner
    {
        Entity Owner { get; }
    }
}