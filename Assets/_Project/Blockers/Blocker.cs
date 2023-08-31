namespace TapTapTap.Blockers
{
    public abstract class Blocker : IBlocker
    {
        public abstract string Reason { get; }

        public virtual bool IsBlocked { get; private set; }

        public void Block()
        {
            IsBlocked = true;
        }

        public void Unblock()
        {
            IsBlocked = false;
        }
    }
}