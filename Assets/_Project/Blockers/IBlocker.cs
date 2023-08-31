namespace TapTapTap.Blockers
{
    public interface IBlocker
    {
        public bool IsBlocked { get; }
        public void Block();
        public void Unblock();
    }
}