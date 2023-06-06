namespace TapTapTap.Core.FSM
{
    public abstract class EntityState : State
    {
        protected new Entity Owner => base.Owner.Owner;
    }
}